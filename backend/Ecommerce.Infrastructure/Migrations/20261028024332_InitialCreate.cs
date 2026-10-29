using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    unit_number = table.Column<string>(type: "text", nullable: false),
                    street_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_line2 = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    parent_category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_category_id",
                        column: x => x.parent_category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    brand_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carts", x => x.id);
                    table.ForeignKey(
                        name: "fk_carts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    order_status = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipping_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    provider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    card_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_methods", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_methods_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_address_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_address_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_colors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_name = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_colors", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_colors_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    image_text = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_sizes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    size_value = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sizes", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_sizes_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    review_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    review_text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shipment_status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipments", x => x.id);
                    table.ForeignKey(
                        name: "fk_shipments_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shipments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    payment_method_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    payment_status = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "addresses",
                columns: new[] { "id", "address_line1", "address_line2", "city", "country", "created_at", "postal_code", "street_number", "unit_number" },
                values: new object[,]
                {
                    { new Guid("63666707-fdf6-4ed2-8862-25d43f400727"), "Terveystie", "", "Houston", "USA", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(2250), "77001", "101 Pine St", "1452" },
                    { new Guid("84ab5536-1e1f-41c6-acf9-9d4506510c62"), "Terveystie", "", "Miami", "USA", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(2252), "33101", "202 Cedar St", "1452" },
                    { new Guid("a7997701-6c21-4608-8995-f5c528da8b5a"), "Terveystie", "", "Chicago", "USA", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(2247), "60601", "789 Oak St", "1452" },
                    { new Guid("cecec44c-a88a-4136-8edf-a665722d22da"), "Terveystie", "", "New York", "USA", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(593), "10001", "123 Main St", "1452" },
                    { new Guid("fb0794f1-f7a6-4bf0-bd19-55803dd283cf"), "Terveystie", "", "Los Angeles", "USA", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(2184), "90001", "456 Elm St", "1452" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_name", "created_at", "parent_category_id" },
                values: new object[,]
                {
                    { new Guid("46b979e2-bf00-4730-a264-ce33cd0bbf0a"), "Men", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(3032), null },
                    { new Guid("49e36633-3d25-4d13-ae15-30c4937a4713"), "Women", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(3484), null },
                    { new Guid("fcadb78b-2748-40ce-bd22-c6dd0189c7f6"), "Kids", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(3490), null }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password_hash", "phone_number", "role", "salt" },
                values: new object[,]
                {
                    { new Guid("07c834f8-546a-4731-86af-b2f59a52a63d"), new DateTime(2026, 10, 28, 2, 43, 31, 373, DateTimeKind.Utc).AddTicks(5899), "email3@example.com", "Bob", "Johnson", "mIU+TUmDtNTBecAArAjHeFvz0N3dxdiho5wmOsRYLX0=", "0413333333", 2, new byte[] { 118, 226, 43, 146, 221, 17, 17, 166, 49, 232, 185, 122, 10, 232, 66, 45 } },
                    { new Guid("23889bce-a3bb-481a-9519-1621abd1f476"), new DateTime(2026, 10, 28, 2, 43, 31, 382, DateTimeKind.Utc).AddTicks(1902), "email5@example.com", "Charlie", "Brown", "+OXVPiAlkwKpmqVyeUr6LrxJCouWUg/gwyWff5/zflU=", "0413333333", 2, new byte[] { 94, 6, 29, 32, 34, 22, 138, 184, 130, 17, 114, 75, 94, 211, 171, 19 } },
                    { new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19"), new DateTime(2026, 10, 28, 2, 43, 31, 369, DateTimeKind.Utc).AddTicks(6024), "email2@example.com", "Jane", "Smith", "0ooD51rjG0Rnzs4mGIOeOrIt0VF1XENVuBD3KtnWmww=", "0413333333", 2, new byte[] { 3, 143, 249, 90, 152, 182, 81, 59, 212, 13, 209, 124, 80, 142, 148, 64 } },
                    { new Guid("412e464b-d704-4129-b8a5-8cfdae332458"), new DateTime(2026, 10, 28, 2, 43, 31, 365, DateTimeKind.Utc).AddTicks(4307), "email1@example.com", "Moh", "nach", "nSsy3lpyLKOv5Pf+SLI8rcSuahrouGXbTFRbSBXP9dc=", "0413333333", 1, new byte[] { 65, 168, 23, 183, 225, 155, 4, 247, 159, 128, 18, 151, 114, 48, 238, 206 } },
                    { new Guid("5e37d628-bdb3-4ecb-b78a-94179df2f4ee"), new DateTime(2026, 10, 28, 2, 43, 31, 385, DateTimeKind.Utc).AddTicks(9550), "email6@example.com", "Oula", "Mok", "JQ3mOV3jbw1YCP+SAG1hF1M6X3o/BfCupUIkbEVUaqc=", "0413333333", 2, new byte[] { 6, 54, 120, 15, 76, 34, 148, 175, 176, 250, 248, 16, 198, 83, 9, 32 } },
                    { new Guid("6fe194bd-0045-41e7-84ff-302f4ef80f30"), new DateTime(2026, 10, 28, 2, 43, 31, 377, DateTimeKind.Utc).AddTicks(3865), "email4@example.com", "Alice", "Williams", "rsKtRlbP5RxKfiFMcqdPN2b2wEuqwoe19yP25bKD4Y4=", "0413333333", 2, new byte[] { 194, 66, 77, 35, 253, 216, 193, 107, 73, 58, 174, 160, 72, 89, 143, 213 } }
                });

            migrationBuilder.InsertData(
                table: "carts",
                columns: new[] { "id", "created_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("547de2d5-ab69-4c7b-beb7-020f22a0b678"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6971), new Guid("6fe194bd-0045-41e7-84ff-302f4ef80f30") },
                    { new Guid("832db372-6d6b-42c5-b497-8f89e32b32b9"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6973), new Guid("23889bce-a3bb-481a-9519-1621abd1f476") },
                    { new Guid("96c7247a-1eef-4847-b6a7-37cd0cb79f1c"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6960), new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19") },
                    { new Guid("a38ff8a4-89b1-46cd-bbbc-f434ff51eff8"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6965), new Guid("07c834f8-546a-4731-86af-b2f59a52a63d") },
                    { new Guid("e4d56e69-bb0b-4e94-ba46-205514ba4eeb"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6718), new Guid("412e464b-d704-4129-b8a5-8cfdae332458") }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_name", "created_at", "parent_category_id" },
                values: new object[,]
                {
                    { new Guid("0669fb68-0cbc-45da-b431-0187c1b76e28"), "WinterWear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4529), new Guid("46b979e2-bf00-4730-a264-ce33cd0bbf0a") },
                    { new Guid("3b74a45b-92cd-4030-84d8-cf24066e9d0a"), "Dresses", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4534), new Guid("49e36633-3d25-4d13-ae15-30c4937a4713") },
                    { new Guid("6925eb47-9b49-4923-a78f-06fe85c25d7c"), "Footwear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4545), new Guid("49e36633-3d25-4d13-ae15-30c4937a4713") },
                    { new Guid("7a4204bf-154c-429a-87a5-a8a53c951ee0"), "EthnicWear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4540), new Guid("49e36633-3d25-4d13-ae15-30c4937a4713") },
                    { new Guid("7fd3d073-14f4-434a-a1a9-ed0bb3b943bd"), "Footwear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4531), new Guid("46b979e2-bf00-4730-a264-ce33cd0bbf0a") },
                    { new Guid("848a4eab-045a-4b5b-af17-1cde1c102d01"), "MaternityWear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4543), new Guid("49e36633-3d25-4d13-ae15-30c4937a4713") },
                    { new Guid("93835b74-b59e-4e12-b692-c5204865d383"), "Bottomwear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4520), new Guid("46b979e2-bf00-4730-a264-ce33cd0bbf0a") },
                    { new Guid("a50a2d87-38c9-41e7-ae1a-070edb0cb5b9"), "School Supplies", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4549), new Guid("fcadb78b-2748-40ce-bd22-c6dd0189c7f6") },
                    { new Guid("cdad8a40-5612-49ae-bb80-d695cd18bffb"), "Topwear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(3492), new Guid("46b979e2-bf00-4730-a264-ce33cd0bbf0a") },
                    { new Guid("f828fd58-a438-4f0b-9447-7c7bf1cc1d7b"), "Toys", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4547), new Guid("fcadb78b-2748-40ce-bd22-c6dd0189c7f6") },
                    { new Guid("f86b0d6f-c32e-486f-b28b-b18d8a52bc35"), "Footwear", new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4580), new Guid("fcadb78b-2748-40ce-bd22-c6dd0189c7f6") }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "address_id", "created_at", "order_date", "order_status", "shipping_address_id", "total_price", "user_id" },
                values: new object[,]
                {
                    { new Guid("27315b46-3f0a-4fb6-8f48-6474bf5d98a2"), null, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(86), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(88), 0, new Guid("84ab5536-1e1f-41c6-acf9-9d4506510c62"), 11m, new Guid("23889bce-a3bb-481a-9519-1621abd1f476") },
                    { new Guid("3699e11f-1e8a-4f2a-ad6f-fb6605abda0b"), null, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(71), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(78), 1, new Guid("fb0794f1-f7a6-4bf0-bd19-55803dd283cf"), 50m, new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19") },
                    { new Guid("412b86e6-ac76-4f21-b4da-48c613f55760"), null, new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(8873), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(9656), 0, new Guid("cecec44c-a88a-4136-8edf-a665722d22da"), 340m, new Guid("412e464b-d704-4129-b8a5-8cfdae332458") },
                    { new Guid("5d26fa49-21b9-465a-938e-d1d8a80989d3"), null, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(83), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(85), 3, new Guid("63666707-fdf6-4ed2-8862-25d43f400727"), 65m, new Guid("6fe194bd-0045-41e7-84ff-302f4ef80f30") },
                    { new Guid("ebe55605-a70c-4f12-9f0e-0be1b87d58bb"), null, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(80), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(82), 2, new Guid("a7997701-6c21-4608-8995-f5c528da8b5a"), 336m, new Guid("07c834f8-546a-4731-86af-b2f59a52a63d") }
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id", "card_number", "created_at", "expiry_date", "payment_type", "provider", "user_id" },
                values: new object[,]
                {
                    { new Guid("230de7cd-b850-4432-bbd0-40f698088e9d"), "14528795", new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3454), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3456), "CreditCard", "MasterCard", new Guid("07c834f8-546a-4731-86af-b2f59a52a63d") },
                    { new Guid("661d29c0-c868-4892-8b56-c2de247fe150"), "1151184", new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1586), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(2416), "CreditCard", "Visa", new Guid("412e464b-d704-4129-b8a5-8cfdae332458") },
                    { new Guid("696c76af-9bdf-4ed6-ae89-8f9e641b3272"), "14528795", new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3459), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3461), "BankTransfer", "Bank", new Guid("23889bce-a3bb-481a-9519-1621abd1f476") },
                    { new Guid("a0c89b74-4ac1-4d12-b3f0-968c3c1acbbf"), "14528795", new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3440), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3453), "CreditCard", "Visa", new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19") },
                    { new Guid("a789ab4c-ab44-43e6-9530-5cb5fd81887a"), "14528795", new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3457), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3458), "PayPal", "PayPal", new Guid("6fe194bd-0045-41e7-84ff-302f4ef80f30") }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_name", "category_id", "created_at", "description", "price", "product_title", "quantity" },
                values: new object[,]
                {
                    { new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), "Adibas", new Guid("46b979e2-bf00-4730-a264-ce33cd0bbf0a"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(4920), "Description", 999.99m, "Laptop XYZ", 3 },
                    { new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), "Adibas", new Guid("fcadb78b-2748-40ce-bd22-c6dd0189c7f6"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6370), "Description", 499.99m, "TV DEF", 3 },
                    { new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), "Adibas", new Guid("49e36633-3d25-4d13-ae15-30c4937a4713"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6357), "Description", 699.99m, "Smartphone ABC", 3 }
                });

            migrationBuilder.InsertData(
                table: "user_address",
                columns: new[] { "id", "address_id", "created_at", "is_default", "user_id" },
                values: new object[,]
                {
                    { new Guid("08e287a2-6a8f-46e3-93b3-31eb4ee46f5f"), new Guid("a7997701-6c21-4608-8995-f5c528da8b5a"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(2240), false, new Guid("6fe194bd-0045-41e7-84ff-302f4ef80f30") },
                    { new Guid("1e2f88bf-3b0c-49ea-b135-7e9f09decd6e"), new Guid("cecec44c-a88a-4136-8edf-a665722d22da"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1599), true, new Guid("412e464b-d704-4129-b8a5-8cfdae332458") },
                    { new Guid("279c4d15-d766-4030-a372-98bc5f451adf"), new Guid("cecec44c-a88a-4136-8edf-a665722d22da"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(2227), false, new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19") },
                    { new Guid("abb2d4c6-2faa-4553-b37e-c177738382c7"), new Guid("fb0794f1-f7a6-4bf0-bd19-55803dd283cf"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(2234), true, new Guid("07c834f8-546a-4731-86af-b2f59a52a63d") },
                    { new Guid("fb2a101f-ea1e-4cb9-8825-cdebd8f71eff"), new Guid("63666707-fdf6-4ed2-8862-25d43f400727"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(2242), true, new Guid("23889bce-a3bb-481a-9519-1621abd1f476") }
                });

            migrationBuilder.InsertData(
                table: "cart_items",
                columns: new[] { "id", "cart_id", "created_at", "product_id", "quantity", "total_price", "unit_price" },
                values: new object[,]
                {
                    { new Guid("5792d314-ebfe-4da7-ae74-e933a2b21cf2"), new Guid("96c7247a-1eef-4847-b6a7-37cd0cb79f1c"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(8215), new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), 1, 19.9m, 19.9m },
                    { new Guid("df2623bb-55f5-4677-b6ab-6ebea4b90974"), new Guid("e4d56e69-bb0b-4e94-ba46-205514ba4eeb"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(7176), new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), 2, 39.8m, 19.9m },
                    { new Guid("f5eead98-3e6a-4131-953f-b764e300669b"), new Guid("a38ff8a4-89b1-46cd-bbbc-f434ff51eff8"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(8245), new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), 3, 59.7m, 19.9m }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "id", "created_at", "order_id", "price", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("0519f66c-94c5-44b3-a299-0e264370f7d0"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1262), new Guid("5d26fa49-21b9-465a-938e-d1d8a80989d3"), 15m, new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), 1 },
                    { new Guid("07481f73-39f5-490b-a78d-438f16d6825d"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1252), new Guid("3699e11f-1e8a-4f2a-ad6f-fb6605abda0b"), 45m, new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), 1 },
                    { new Guid("c84015eb-c1dc-4748-b6e1-8d7e789e9e69"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1242), new Guid("412b86e6-ac76-4f21-b4da-48c613f55760"), 15m, new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), 1 },
                    { new Guid("c8f4b3f5-9ab1-476f-821a-4c6d428c8ebd"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1264), new Guid("5d26fa49-21b9-465a-938e-d1d8a80989d3"), 5m, new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), 3 },
                    { new Guid("cd69539a-753a-491f-a2d6-028a451eb8ce"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1249), new Guid("3699e11f-1e8a-4f2a-ad6f-fb6605abda0b"), 5m, new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), 3 },
                    { new Guid("f76128a1-91d5-4165-8a77-4f4a56b2f9fe"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1266), new Guid("5d26fa49-21b9-465a-938e-d1d8a80989d3"), 45m, new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), 1 }
                });

            migrationBuilder.InsertData(
                table: "payments",
                columns: new[] { "id", "amount", "created_at", "order_id", "payment_date", "payment_method_id", "payment_status", "user_id" },
                values: new object[,]
                {
                    { new Guid("0504f1fd-d957-4973-b135-5e75369535d8"), 399.99m, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5083), new Guid("5d26fa49-21b9-465a-938e-d1d8a80989d3"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5089), new Guid("661d29c0-c868-4892-8b56-c2de247fe150"), 1, new Guid("23889bce-a3bb-481a-9519-1621abd1f476") },
                    { new Guid("28d3761b-daf8-41ca-ad10-672d7c4ce4ba"), 49.99m, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5080), new Guid("ebe55605-a70c-4f12-9f0e-0be1b87d58bb"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5082), new Guid("661d29c0-c868-4892-8b56-c2de247fe150"), 1, new Guid("07c834f8-546a-4731-86af-b2f59a52a63d") },
                    { new Guid("7c01f880-b51f-4599-bb3b-5f69bd60a4f9"), 89.99m, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5078), new Guid("3699e11f-1e8a-4f2a-ad6f-fb6605abda0b"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5080), new Guid("661d29c0-c868-4892-8b56-c2de247fe150"), 1, new Guid("412e464b-d704-4129-b8a5-8cfdae332458") },
                    { new Guid("ae0c94e3-f1d2-48bd-880e-8bd2bf54f174"), 299.99m, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(3822), new Guid("27315b46-3f0a-4fb6-8f48-6474bf5d98a2"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(4465), new Guid("661d29c0-c868-4892-8b56-c2de247fe150"), 1, new Guid("412e464b-d704-4129-b8a5-8cfdae332458") },
                    { new Guid("d85a5ace-39a5-444d-80b8-79d023e2407f"), 149.99m, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5070), new Guid("412b86e6-ac76-4f21-b4da-48c613f55760"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5076), new Guid("661d29c0-c868-4892-8b56-c2de247fe150"), 1, new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19") }
                });

            migrationBuilder.InsertData(
                table: "product_colors",
                columns: new[] { "id", "color_name", "created_at", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("0d0017be-28f3-44b1-8bba-dc4a207fc32a"), 1, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(7047), new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), 5 },
                    { new Guid("e23f3815-3180-4b0f-9d98-473781d8ccd8"), 2, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6455), new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), 15 },
                    { new Guid("e6cfb834-ad03-4d89-87bb-ef67d34aa28c"), 5, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(7053), new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), 3 }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "id", "created_at", "image_text", "image_url", "is_default", "product_id" },
                values: new object[,]
                {
                    { new Guid("2e5358b9-5db1-4d76-afa5-bdf19b7a7f9c"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6235), "Sample product image", "https://picsum.photos/200/300", false, new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59") },
                    { new Guid("a761afce-6be6-4ea8-8b50-2128f2bd7e54"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(5375), "Sample product image", "https://picsum.photos/200/300", true, new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59") },
                    { new Guid("b2f5ed4a-a164-41ad-99d6-4047a3fc0698"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6242), "Sample product image", "https://picsum.photos/200/300", false, new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59") },
                    { new Guid("bdab1189-3a1f-4116-9751-4ee4775477c6"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6246), "Sample product image", "https://picsum.photos/200/300", true, new Guid("d673f183-4495-459f-9a29-cb142a700a7d") },
                    { new Guid("cee3c1e4-2336-4d46-84ab-83780c1bc21e"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6248), "Sample product image", "https://picsum.photos/200/300", false, new Guid("d673f183-4495-459f-9a29-cb142a700a7d") },
                    { new Guid("e12f2e47-2a93-44b2-8f0e-abd803a53b38"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6237), "Sample product image", "https://picsum.photos/200/300", true, new Guid("7c417a29-1343-4b15-baba-7331bc606a91") }
                });

            migrationBuilder.InsertData(
                table: "product_sizes",
                columns: new[] { "id", "created_at", "product_id", "quantity", "size_value" },
                values: new object[,]
                {
                    { new Guid("06bda0de-b591-4b64-81db-45bf82da43d3"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(8046), new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), 10, 3 },
                    { new Guid("663eca57-05f5-48e1-9043-3717eb90aae3"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(8014), new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), 3, 2 },
                    { new Guid("b03fe898-cda7-4f1b-91f4-67697418c4d2"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(7293), new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "brand_name", "category_id", "created_at", "description", "price", "product_title", "quantity" },
                values: new object[,]
                {
                    { new Guid("4c1fc896-6a4a-4ae8-933d-47c1eaba5e2c"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6439), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("52c9c605-846c-4d9d-b32f-0c6386a0e960"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6472), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("6537f87d-d835-48fe-83da-f7eb17a17b0b"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6475), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("7fcc7ad7-014c-409a-8204-92ee3f60c030"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6441), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("94cf8a58-d8c7-4b62-b6a9-0ba6e51842f1"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6463), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6437), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("dcea0254-899d-4053-a412-6cd7c67b1d63"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6468), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("eb51cd99-4a38-41b1-826d-05a6f8ce2906"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6470), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("f2716d05-39ed-42cd-96e7-9fb021607299"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6445), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("f50a49a2-d92f-4705-89e4-ac063357922e"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6443), "Description", 149.99m, "Headphones JKL", 3 },
                    { new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), "Adibas", new Guid("cdad8a40-5612-49ae-bb80-d695cd18bffb"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6434), "Description", 299.99m, "Tablet GHI", 3 },
                    { new Guid("ff1e1d10-7eb3-4111-a11e-307d7036192c"), "Adibas", new Guid("93835b74-b59e-4e12-b692-c5204865d383"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(6477), "Description", 149.99m, "Headphones JKL", 3 }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "id", "created_at", "product_id", "rating", "review_date", "review_text", "user_id" },
                values: new object[,]
                {
                    { new Guid("071142f7-fddb-46b8-bbd8-92d791588206"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9714), new Guid("d673f183-4495-459f-9a29-cb142a700a7d"), 4, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9723), "Very good.", new Guid("3ed8de70-1ca9-4bd3-a48f-22af41d5bb19") },
                    { new Guid("1aa06846-6589-451d-a5c2-ad14bd499ccc"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(8339), new Guid("480f787a-fc13-46b3-9d39-bc9553f61b59"), 5, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9176), "Excellent!", new Guid("412e464b-d704-4129-b8a5-8cfdae332458") },
                    { new Guid("dec22867-6747-43ed-bdbd-c1b83a2e0077"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9724), new Guid("7c417a29-1343-4b15-baba-7331bc606a91"), 3, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9727), "Average.", new Guid("07c834f8-546a-4731-86af-b2f59a52a63d") }
                });

            migrationBuilder.InsertData(
                table: "shipments",
                columns: new[] { "id", "address_id", "created_at", "order_id", "shipment_date", "shipment_status" },
                values: new object[,]
                {
                    { new Guid("47311b7b-c820-470e-9217-881dd45bbe33"), new Guid("a7997701-6c21-4608-8995-f5c528da8b5a"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1342), new Guid("ebe55605-a70c-4f12-9f0e-0be1b87d58bb"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1343), 2 },
                    { new Guid("47ac513f-71ec-45c2-86d8-0901af615c05"), new Guid("cecec44c-a88a-4136-8edf-a665722d22da"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(94), new Guid("412b86e6-ac76-4f21-b4da-48c613f55760"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(406), 0 },
                    { new Guid("b8a668a6-45de-46e5-aa23-9792374a8bbc"), new Guid("63666707-fdf6-4ed2-8862-25d43f400727"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1345), new Guid("5d26fa49-21b9-465a-938e-d1d8a80989d3"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1346), 3 },
                    { new Guid("bc547b10-e667-47cb-8b50-b249fd7b3097"), new Guid("84ab5536-1e1f-41c6-acf9-9d4506510c62"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1347), new Guid("27315b46-3f0a-4fb6-8f48-6474bf5d98a2"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1349), 1 },
                    { new Guid("ed11357d-a3c6-4b5d-905d-5efca6e94e5d"), new Guid("fb0794f1-f7a6-4bf0-bd19-55803dd283cf"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1335), new Guid("3699e11f-1e8a-4f2a-ad6f-fb6605abda0b"), new DateTime(2026, 10, 28, 2, 43, 31, 388, DateTimeKind.Utc).AddTicks(1341), 4 }
                });

            migrationBuilder.InsertData(
                table: "cart_items",
                columns: new[] { "id", "cart_id", "created_at", "product_id", "quantity", "total_price", "unit_price" },
                values: new object[,]
                {
                    { new Guid("bb22724c-5b44-4f32-9058-5f6011957746"), new Guid("832db372-6d6b-42c5-b497-8f89e32b32b9"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(8251), new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), 4, 79.6m, 19.9m },
                    { new Guid("fd27faee-ccf6-4035-94d6-11fc96ddb23b"), new Guid("547de2d5-ab69-4c7b-beb7-020f22a0b678"), new DateTime(2026, 10, 28, 2, 43, 31, 386, DateTimeKind.Utc).AddTicks(8248), new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), 1, 19.9m, 19.9m }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "id", "created_at", "order_id", "price", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("7b13be4e-c61b-4549-ac2d-dab475ae873c"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1260), new Guid("ebe55605-a70c-4f12-9f0e-0be1b87d58bb"), 325m, new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), 2 },
                    { new Guid("8d02e3fe-e44e-43fd-9b47-b2daa93d3afc"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1268), new Guid("27315b46-3f0a-4fb6-8f48-6474bf5d98a2"), 11m, new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), 4 },
                    { new Guid("d227d5d5-2419-4c33-8fd2-40a11c4a34d4"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(1258), new Guid("ebe55605-a70c-4f12-9f0e-0be1b87d58bb"), 11m, new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), 4 },
                    { new Guid("fde25235-874c-4820-91fe-4a6d0eecfbb4"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(331), new Guid("412b86e6-ac76-4f21-b4da-48c613f55760"), 325m, new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), 2 }
                });

            migrationBuilder.InsertData(
                table: "product_colors",
                columns: new[] { "id", "color_name", "created_at", "product_id", "quantity" },
                values: new object[,]
                {
                    { new Guid("0fc6f326-eb9d-422e-b982-64a799133a7b"), 0, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(7056), new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), 6 },
                    { new Guid("bdf53fe1-5c86-4963-bba4-11c9c341ca6d"), 3, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(7058), new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), 4 }
                });

            migrationBuilder.InsertData(
                table: "product_images",
                columns: new[] { "id", "created_at", "image_text", "image_url", "is_default", "product_id" },
                values: new object[,]
                {
                    { new Guid("45bb201a-d702-4614-aae9-ef432651df99"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6254), "Sample product image", "https://picsum.photos/200/300", false, new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e") },
                    { new Guid("e7ec68fe-f372-4a16-8470-68efc1cde675"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6228), "Sample product image", "https://picsum.photos/200/300", true, new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9") },
                    { new Guid("fb3fc749-fe74-438f-85c0-2d181e5e4752"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(6244), "Sample product image", "https://picsum.photos/200/300", true, new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e") }
                });

            migrationBuilder.InsertData(
                table: "product_sizes",
                columns: new[] { "id", "created_at", "product_id", "quantity", "size_value" },
                values: new object[,]
                {
                    { new Guid("74c98ba8-2abf-49af-a3c7-4ea6498c70bc"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(8050), new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), 9, 0 },
                    { new Guid("da9e18fe-2ce4-454a-aed6-8ee7c718f467"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(8049), new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), 14, 1 }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "id", "created_at", "product_id", "rating", "review_date", "review_text", "user_id" },
                values: new object[,]
                {
                    { new Guid("711c4698-3b95-4f72-ac27-a933b6d03bf1"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9730), new Guid("d8a67080-f697-4a62-9fc3-0cc2a5fe057e"), 1, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9738), "Terrible!", new Guid("23889bce-a3bb-481a-9519-1621abd1f476") },
                    { new Guid("fa77752c-2934-403a-931d-1ea514c4c2c1"), new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9727), new Guid("fa31f50d-c801-46f0-b86f-d930a75edbe9"), 2, new DateTime(2026, 10, 28, 2, 43, 31, 387, DateTimeKind.Utc).AddTicks(9730), "Not great.", new Guid("6fe194bd-0045-41e7-84ff-302f4ef80f30") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_product_id",
                table: "cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_carts_user_id",
                table: "carts",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_category_id",
                table: "categories",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_methods_user_id",
                table: "payment_methods",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_order_id",
                table: "payments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_payment_method_id",
                table: "payments",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_user_id",
                table: "payments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_colors_product_id",
                table: "product_colors",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_images_product_id",
                table: "product_images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_sizes_product_id",
                table: "product_sizes",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipments_address_id",
                table: "shipments",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipments_order_id",
                table: "shipments",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_address_address_id",
                table: "user_address",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_address_user_id",
                table: "user_address",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "product_colors");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "product_sizes");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "shipments");

            migrationBuilder.DropTable(
                name: "user_address");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
