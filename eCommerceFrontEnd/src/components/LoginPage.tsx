import { useState, type FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";
import { API_BASE } from "../api";
import type { AuthSession } from "../context/AuthContext";

interface LoginResponse {
  accessToken: string;
  tokenType: string;
  expiresIn: number;
  userId: number;
  cartId?: number;
  username: string;
  displayName: string;
}

interface LoginPageProps {
  onLogin: (session: AuthSession) => void;
}

function isLoginResponse(value: unknown): value is LoginResponse {
  if (typeof value !== "object" || value === null) return false;
  const response = value as Record<string, unknown>;
  return (
    typeof response.accessToken === "string" &&
    typeof response.tokenType === "string" &&
    typeof response.expiresIn === "number" &&
    typeof response.userId === "number" &&
    (response.cartId === undefined || typeof response.cartId === "number") &&
    typeof response.username === "string" &&
    typeof response.displayName === "string"
  );
}

export default function LoginPage({ onLogin }: LoginPageProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const navigate = useNavigate();

  const submitLogin = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setError("");
    setSubmitting(true);

    try {
      const response = await fetch(`${API_BASE}/auth/login`, {
        method: "POST",
        headers: {
          Accept: "text/plain",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const details = await response.text();
        throw new Error(details || `Đăng nhập thất bại (${response.status})`);
      }

      const data: unknown = await response.json();
      if (!isLoginResponse(data)) {
        throw new Error("Dữ liệu đăng nhập trả về không hợp lệ");
      }

      onLogin({
        accessToken: data.accessToken,
        tokenType: data.tokenType,
        expiresAt: Date.now() + data.expiresIn * 1000,
        userId: data.userId,
        cartId: data.cartId ?? 0,
        username: data.username,
        displayName: data.displayName,
      });
      navigate("/", { replace: true });
    } catch (err) {
      setError(err instanceof Error ? err.message : "Đăng nhập thất bại");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <main style={{ maxWidth: 400, margin: "48px auto", padding: 20 }}>
      <h1>Đăng nhập</h1>
      <form
        onSubmit={(event) => void submitLogin(event)}
        style={{ display: "grid", gap: 14 }}
      >
        <label style={{ display: "grid", gap: 6 }}>
          Tên đăng nhập
          <input
            autoComplete="username"
            required
            value={username}
            onChange={(event) => setUsername(event.target.value)}
          />
        </label>
        <label style={{ display: "grid", gap: 6 }}>
          Mật khẩu
          <input
            autoComplete="current-password"
            required
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
          />
        </label>
        {error && <p role="alert" style={{ color: "red" }}>{error}</p>}
        <button type="submit" disabled={submitting}>
          {submitting ? "Đang đăng nhập..." : "Đăng nhập"}
        </button>
        <Link to="/">Quay lại cửa hàng</Link>
      </form>
    </main>
  );
}
