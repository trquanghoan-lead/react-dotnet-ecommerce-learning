import type { AuthSession } from "./context/AuthContext";

export const API_BASE = "https://localhost:7000/api";

export function authorizationHeaders(
  session: AuthSession,
): Record<string, string> {
  return {
    Authorization: `${session.tokenType} ${session.accessToken}`,
  };
}
