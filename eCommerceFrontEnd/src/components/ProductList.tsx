import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE, authorizationHeaders } from "../api";
import type { AuthSession } from "../context/AuthContext";

interface Product {
  id: number;
  name: string;
  price: number;
  stock: number;
}

interface ProductListProps {
  session: AuthSession | null;
  onAddedToCart: () => void;
}

export default function ProductList({
  session,
  onAddedToCart,
}: ProductListProps) {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [addingProductId, setAddingProductId] = useState<number | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const controller = new AbortController();

    fetch(`${API_BASE}/products`, { signal: controller.signal })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error(`Không tải được sản phẩm (${response.status})`);
        }
        return (await response.json()) as Product[];
      })
      .then(setProducts)
      .catch((err: unknown) => {
        if (!controller.signal.aborted) {
          setError(
            err instanceof Error ? err.message : "Không tải được sản phẩm",
          );
        }
      })
      .finally(() => {
        if (!controller.signal.aborted) setLoading(false);
      });

    return () => controller.abort();
  }, []);

  const addToCart = async (productId: number) => {
    if (!session) {
      navigate("/login");
      return;
    }

    setAddingProductId(productId);
    setError("");
    try {
      const response = await fetch(`${API_BASE}/cart/items`, {
        method: "POST",
        headers: {
          ...authorizationHeaders(session),
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          cartId: session.cartId ?? 0,
          productId,
          quantity: 1,
        }),
      });

      if (!response.ok) {
        const details = await response.text();
        throw new Error(
          details || `Không thể thêm sản phẩm vào giỏ (${response.status})`,
        );
      }

      onAddedToCart();
    } catch (err) {
      setError(
        err instanceof Error ? err.message : "Không thể thêm sản phẩm vào giỏ",
      );
    } finally {
      setAddingProductId(null);
    }
  };

  if (loading) return <p>Đang tải sản phẩm...</p>;
  if (error && products.length === 0) {
    return <p style={{ color: "red" }}>{error}</p>;
  }

  return (
    <>
      <h3 style={{ marginBottom: 16 }}>Sản phẩm</h3>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <div style={{ display: "flex", flexWrap: "wrap", gap: 16 }}>
        {products.map((product) => (
          <article
            key={product.id}
            style={{
              border: "1px solid #e0e0e0",
              borderRadius: 8,
              padding: 16,
              width: 220,
              boxShadow: "0 2px 6px rgba(0,0,0,0.08)",
              display: "flex",
              flexDirection: "column",
              justifyContent: "space-between",
            }}
          >
            <div>
              <h4 style={{ margin: "0 0 8px" }}>{product.name}</h4>
              <p style={{ margin: "4px 0" }}>
                <b>₹{product.price}</b>
              </p>
              <p style={{ margin: "4px 0", color: "#666" }}>
                Tồn kho: {product.stock}
              </p>
            </div>
            <button
              disabled={addingProductId === product.id}
              onClick={() => void addToCart(product.id)}
              style={{
                marginTop: 12,
                padding: 8,
                borderRadius: 6,
                border: "none",
                backgroundColor: "#2563eb",
                color: "white",
                cursor: "pointer",
              }}
            >
              {addingProductId === product.id ? "Đang thêm..." : "Thêm vào giỏ"}
            </button>
          </article>
        ))}
      </div>
    </>
  );
}
