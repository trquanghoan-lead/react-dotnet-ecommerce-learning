import {
  createContext,
  useContext,
  useState,
  type ReactNode,
} from "react";

const AUTH_STORAGE_KEY = "ecommerce-auth-session";

export interface AuthSession {
  accessToken: string;
  tokenType: string;
  expiresAt: number;
  userId: number;
  cartId: number;
  username: string;
  displayName: string;
}

interface AuthContextValue {
  session: AuthSession | null;
  login: (session: AuthSession) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextValue | null>(null);

function readStoredSession(): AuthSession | null {
  const stored = localStorage.getItem(AUTH_STORAGE_KEY);
  if (!stored) return null;

  try {
    const session: unknown = JSON.parse(stored);
    if (
      typeof session === "object" &&
      session !== null &&
      "accessToken" in session &&
      typeof session.accessToken === "string" &&
      "tokenType" in session &&
      typeof session.tokenType === "string" &&
      "expiresAt" in session &&
      typeof session.expiresAt === "number" &&
      session.expiresAt > Date.now() &&
      "userId" in session &&
      typeof session.userId === "number" &&
      (!("cartId" in session) || typeof session.cartId === "number") &&
      "username" in session &&
      typeof session.username === "string" &&
      "displayName" in session &&
      typeof session.displayName === "string"
    ) {
      const cartId =
        "cartId" in session && typeof session.cartId === "number"
          ? session.cartId
          : 0;

      return {
        accessToken: session.accessToken,
        tokenType: session.tokenType,
        expiresAt: session.expiresAt,
        userId: session.userId,
        cartId,
        username: session.username,
        displayName: session.displayName,
      };
    }
  } catch {
    localStorage.removeItem(AUTH_STORAGE_KEY);
    return null;
  }

  localStorage.removeItem(AUTH_STORAGE_KEY);
  return null;
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [session, setSession] = useState<AuthSession | null>(readStoredSession);

  const login = (newSession: AuthSession) => {
    localStorage.setItem(AUTH_STORAGE_KEY, JSON.stringify(newSession));
    setSession(newSession);
  };

  const logout = () => {
    localStorage.removeItem(AUTH_STORAGE_KEY);
    setSession(null);
  };

  return (
    <AuthContext.Provider value={{ session, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth(): AuthContextValue {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider");
  }
  return context;
}
