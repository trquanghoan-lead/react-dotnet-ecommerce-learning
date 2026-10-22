# E-Commerce Checkout Application

This repository contains a **full-stack e-commerce checkout system** built using:

- **Backend**: ASP.NET Core Web API
- **Frontend**: React (Vite)

The application allows users to browse products, add items to a cart, apply coupons, and place an order with proper validation and pricing logic.

---

## 📁 Project Structure
ecommerce-app/
│
├── backend/
│ └── ECommerceBackend/
│ ├── Controllers/
│ ├── Services/
│ ├── Repository/
│ ├── Models/
│ ├── Database/
│ │ ├── products.json
│ │ └── coupons.json
│ └── ECommerceBackend.csproj
│
├── frontend/
│ └── ecommerce-ui/
│ ├── src/
│ ├── public/
│ └── package.json
│
└── README.md


---

## 🖥️ Backend – ASP.NET Core Web API

### Features
- Product listing API
- Cart management (add/update items)
- Coupon application
- Checkout with pricing summary
- Order retrieval
- Swagger / OpenAPI enabled
- xUnit unit tests (service layer)

### Key APIs
- `GET /api/products`
- `POST /api/cart/items`
- `GET /api/cart/{cartId}`
- `POST /api/cart/{cartId}/apply-coupon`
- `POST /api/cart/{cartId}/checkout`
- `GET /api/orders/{orderId}`

---

### 🗄️ Data Handling (Important Design Note)

To keep the project **lightweight and easy to run**, no real database is used.

#### Product & Coupon Data
- Stored as **dummy JSON files** inside the `Database` folder:
  - `products.json`
  - `coupons.json`
- These files simulate a database and are loaded via the repository layer.

#### Cart & Order State
- **In-memory `Dictionary`** is used to maintain:
  - Active cart
  - Orders
- This allows fast prototyping while still enforcing:
  - Stock validation
  - Coupon rules
  - Atomic checkout logic

> This approach makes the project easy to replicate locally without external dependencies, while still demonstrating proper backend architecture.

---

### 🧱 Backend Architecture

- **Controllers**: Handle HTTP requests only
- **Services**: Business logic & validations
- **Repositories**: Data access (JSON-based)
- **Models**: Domain entities (Product, Cart, Order, Coupon)

---

### ▶️ Run Backend

```bash
cd backend/ECommerceBackend
dotnet restore
dotnet run
"# react-dotnet-ecommerce-learning" 
