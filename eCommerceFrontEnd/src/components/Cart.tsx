import { useEffect, useState } from "react";
import { API_BASE, authorizationHeaders } from "../api";
import type { AuthSession } from "../context/AuthContext";
import { styles } from "../styles";

interface CartItem {
  productId: number;
  quantity: number;
}

interface ShoppingCart {
  items: CartItem[];
}

interface Order {
  subTotal: number;
  discount: number;
  grandTotal: number;
}

interface CartProps {
  cartId: number;
  session: AuthSession;
  refreshKey: number;
}

export default function Cart({ cartId, session, refreshKey }: CartProps) {
  const [cart, setCart] = useState<ShoppingCart | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [coupon, setCoupon] = useState("");
  const [message, setMessage] = useState("");
  const [order, setOrder] = useState<Order | null>(null);

  useEffect(() => {
    const controller = new AbortController();
    setLoading(true);
    setError("");

    fetch(`${API_BASE}/cart/${cartId}`, {
      headers: authorizationHeaders(session),
      signal: controller.signal,
    })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error(`Không tải được giỏ hàng (${response.status})`);
        }
        return (await response.json()) as ShoppingCart;
      })
      .then(setCart)
      .catch((err: unknown) => {
        if (!controller.signal.aborted) {
          setError(
            err instanceof Error ? err.message : "Không tải được giỏ hàng",
          );
        }
      })
      .finally(() => {
        if (!controller.signal.aborted) setLoading(false);
      });

    return () => controller.abort();
  }, [cartId, refreshKey, session]);

  const applyCoupon = async () => {
    setMessage("");
    try {
      const response = await fetch(
        `${API_BASE}/cart/${cartId}/apply-coupon?code=${encodeURIComponent(coupon)}`,
        {
          method: "POST",
          headers: authorizationHeaders(session),
        },
      );
      if (!response.ok) {
        setMessage("Mã giảm giá không hợp lệ");
        return;
      }
      setCart((await response.json()) as ShoppingCart);
      setMessage("Áp dụng mã giảm giá thành công");
    } catch (err) {
      setMessage(
        err instanceof Error ? err.message : "Không thể áp dụng mã giảm giá",
      );
    }
  };

  const checkout = async () => {
    setMessage("");
    try {
      const response = await fetch(
        `${API_BASE}/cart/${cartId}/checkout`,
        {
          method: "POST",
          headers: authorizationHeaders(session),
        },
      );
      if (!response.ok) {
        setMessage("Thanh toán thất bại");
        return;
      }
      setOrder((await response.json()) as Order);
    } catch (err) {
      setMessage(err instanceof Error ? err.message : "Thanh toán thất bại");
    }
  };

  if (order) {
    return (
      <div style={styles.card}>
        <h2>Đặt hàng thành công</h2>
        <div style={styles.summaryRow}>
          <span>Tạm tính</span>
          <span>₹{order.subTotal}</span>
        </div>
        <div style={styles.summaryRow}>
          <span>Giảm giá</span>
          <span>- ₹{order.discount}</span>
        </div>
        <hr />
        <div style={{ ...styles.summaryRow, fontWeight: "bold" }}>
          <span>Tổng thanh toán</span>
          <span>₹{order.grandTotal}</span>
        </div>
      </div>
    );
  }

  if (loading) return <p>Đang tải giỏ hàng...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;
  if (!cart) return null;
  if (cart.items.length === 0) return <p>🛒 Giỏ hàng đang trống</p>;

  return (
    <div style={styles.card}>
      <h2>Giỏ hàng của bạn</h2>
      <div style={{ ...styles.itemsContainer, flexDirection: "column" }}>
        {cart.items.map((item) => (
          <div key={item.productId} style={styles.itemRow}>
            <span>Sản phẩm #{item.productId}</span>
            <span>Số lượng: {item.quantity}</span>
          </div>
        ))}
      </div>

      <div style={styles.couponRow}>
        <input
          style={styles.input}
          placeholder="Nhập mã giảm giá"
          value={coupon}
          onChange={(event) => setCoupon(event.target.value)}
        />
        <button style={styles.secondaryBtn} onClick={() => void applyCoupon()}>
          Áp dụng
        </button>
      </div>

      <button style={styles.primaryBtn} onClick={() => void checkout()}>
        Thanh toán
      </button>
      {message && (
        <p style={{ ...styles.message, textAlign: "center" }}>{message}</p>
      )}
    </div>
  );
}
