import * as Icons from "lucide-react";

const sidebarItems = [
    {
        title: "Dashboard",
        icon:Icons.LayoutGrid,
        link: "/",
    },

    {
        title: "Sales",
        icon: Icons.ShoppingCart,
        children: [
            {
                title: "Quotations",
                link: "/sales/quotations",
                icon: Icons.FileSpreadsheet,
            },
            {
                title: "Sales Orders",
                link: "/sales/orders",
                icon: Icons.ClipboardList,
            },
            {
                title: "Invoices",
                link: "/sales/invoices",
                icon: Icons.ReceiptText,
            },
            {
                title: "Credit Notes",
                link: "/sales/credit-notes",
                icon: Icons.FileClock,
            },
        ],
    },

    {
        title: "Purchasing",
        icon: Icons.BadgeDollarSign,
        children: [
            {
                title: "Purchase Requests",
                link: "/purchase/requests",
                icon: Icons.ClipboardList,
            },
            {
                title: "Purchase Orders",
                link: "/purchase/orders",
                icon: Icons. FileSpreadsheet,
            },
            {
                title: "Supplier Bills",
                link: "/purchase/bills",
                icon: Icons.ReceiptText,
            },
            {
                title: "Purchase Returns",
                link: "/purchase/returns",
                icon:Icons. FileClock,
            },
        ],
    },

    {
        title: "Inventory",
        icon: Icons.Warehouse,
        children: [
            {
                title: "Products",
                link: "/inventory/products",
                icon: Icons.Package,
            },
            {
                title: "Categories",
                link: "/inventory/categories",
                icon: Icons.Boxes,
            },
            {
                title: "Stock",
                link: "/inventory/stock",
                icon: Icons.Warehouse,
            },
            {
                title: "Stock Adjustment",
                link: "/inventory/adjustments",
                icon: Icons.ClipboardList,
            },
        ],
    },

    {
        title: "Customers",
        icon: Icons.UserRound,
        children: [
            {
                title: "Customer List",
                link: "/customers",
                icon: Icons.Users,
            },
            {
                title: "Customer Groups",
                link: "/customers/groups",
                icon: Icons.Boxes,
            },
        ],
    },

    {
        title: "Suppliers",
        icon: Icons.Building2,
        children: [
            {
                title: "Supplier List",
                link: "/suppliers",
                icon: Icons.Building2,
            },
            {
                title: "Supplier Groups",
                link: "/suppliers/groups",
                icon: Icons.Boxes,
            },
        ],
    },

    {
        title: "Finance",
        icon: Icons.Landmark,
        children: [
            {
                title: "Chart of Accounts",
                link: "/finance/accounts",
                icon: Icons.Landmark,
            },
            {
                title: "Payments",
                link: "/finance/payments",
                icon: Icons.Wallet,
            },
            {
                title: "Expenses",
                link: "/finance/expenses",
                icon: Icons.ReceiptText,
            },
            {
                title: "Taxes",
                link: "/finance/taxes",
                icon: Icons.FileSpreadsheet,
            },
        ],
    },

    {
        title: "Reports",
        icon: Icons.BarChart3,
        children: [
            {
                title: "Sales Report",
                link: "/reports/sales",
                icon: Icons.ShoppingCart,
            },
            {
                title: "Purchase Report",
                link: "/reports/purchases",
                icon: Icons.BadgeDollarSign,
            },
            {
                title: "Inventory Report",
                link: "/reports/inventory",
                icon: Icons.Warehouse,
            },
            {
                title: "Financial Report",
                link: "/reports/finance",
                icon:Icons.Landmark,
            },
        ],
    },

    {
        title: "Administration",
        icon: Icons.Shield,
        children: [
            {
                title: "Users",
                link: "/admin/users",
                icon: Icons.Users,
            },
            {
                title: "Roles & Permissions",
                link: "/admin/roles",
                icon: Icons.Shield,
            },
            {
                title: "Settings",
                link: "/admin/settings",
                icon: Icons.Settings,
            },
        ],
    },

    {
        title: "Notifications",
        icon: Icons.Bell,
        link: "/notifications",
    },

    {
        title: "Help",
        icon: Icons.CircleHelp,
        link: "/help",
    },
];

export default sidebarItems;