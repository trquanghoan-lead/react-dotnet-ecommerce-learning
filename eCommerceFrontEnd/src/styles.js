export const styles= {
  card: {
    maxWidth: 420,
    margin: "20px auto",
    padding: 20,
    borderRadius: 10,
    border: "1px solid #ddd",
    boxShadow: "0 4px 10px rgba(0,0,0,0.08)"
  },
  itemsContainer: {
    display: "flex",
    flexDirection: "column",
    gap: 10,
    marginBottom: 15
  },
  itemRow: {
    display: "flex",
    justifyContent: "space-between",
    padding: "6px 0",
    borderBottom: "1px solid #eee"
  },
  couponRow: {
    display: "flex",
    gap: 10,
    marginBottom: 15
  },
  input: {
    flex: 1,
    padding: 8,
    borderRadius: 6,
    border: "1px solid #ccc"
  },
  primaryBtn: {
    width: "100%",
    padding: 10,
    backgroundColor: "#4f46e5",
    color: "#fff",
    border: "none",
    borderRadius: 6,
    cursor: "pointer",
    fontWeight: "bold"
  },
  secondaryBtn: {
    padding: "8px 12px",
    borderRadius: 6,
    border: "1px solid #4f46e5",
    background: "#fff",
    cursor: "pointer"
  },
  message: {
    marginTop: 12,
    textAlign: "center"
  },
  summaryRow: {
    display: "flex",
    justifyContent: "space-between",
    marginBottom: 8
  }
};
