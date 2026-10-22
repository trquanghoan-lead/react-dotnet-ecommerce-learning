import { useState } from "react";
import {
  BrowserRouter,
  Navigate,
  Route,
  Routes,
  useNavigate,
} from "react-router-dom";
import Cart from "./components/Cart";
import LoginPage from "./components/LoginPage";
import ProductList from "./components/ProductList";
import { AuthProvider, useAuth } from "./context/AuthContext";

function StorePage() {
  const { session, logout } = useAuth();
  const [cartRefreshKey, setCartRefreshKey] = useState(0);
  const navigate = useNavigate();
  const cartId = session?.cartId ?? 0;

  return (
    <main style={{ padding: 20 }}>
      <header
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
          gap: 16,
          marginBottom: 24,
        }}
      >
        <h2>E-Commerce Store</h2>
        {session ? (
          <div style={{ display: "flex", alignItems: "center", gap: 12 }}>
            <span>Xin chào, {session.displayName}</span>
            <button onClick={logout}>Đăng xuất</button>
          </div>
        ) : (
          <button onClick={() => navigate("/login")}>Đăng nhập</button>
        )}
      </header>

      <ProductList
        session={session}
        onAddedToCart={() => setCartRefreshKey((key) => key + 1)}
      />
      {session && (
        <>
          <hr />
          <Cart
            cartId={cartId}
            session={session}
            refreshKey={cartRefreshKey}
          />
        </>
      )}
    </main>
  );
}

function AppRoutes() {
  const { session, login } = useAuth();

  return (
    <Routes>
      <Route path="/" element={<StorePage />} />
      <Route
        path="/login"
        element={
          session ? (
            <Navigate to="/" replace />
          ) : (
            <LoginPage onLogin={login} />
          )
        }
      />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <AppRoutes />
      </AuthProvider>
    </BrowserRouter>
  );
}
