# 🌐 PrimeLedger ERP

> **A modern, enterprise‑grade ERP and Accounting System** built to model real‑world financial operations with a strong emphasis on **scalability, auditability, and clean architecture.**

---

## 📖 Overview

**PrimeLedger** is a production‑inspired ERP and Accounting platform designed to demonstrate how complex enterprise financial systems are architected and developed.  

Unlike simple CRUD applications, PrimeLedger models **complete business processes**—from procurement and sales to inventory, taxation, and general ledger accounting. Every design decision emphasizes **maintainability, extensibility, and auditability**, making it both a **learning platform** and a **portfolio project** for enterprise software engineering.

---

## 🎯 Objectives

- Architect an enterprise‑grade ERP system  
- Model real‑world accounting and financial workflows  
- Implement scalable backend services with clean architecture  
- Build an audit‑friendly financial platform  
- Explore database design, optimization, and multi‑tenant strategies  
- Showcase modern software engineering best practices  

---

## 🏗 Core Modules

### Financial Management
- General Ledger (GL), Chart of Accounts, Journal Entries  
- Double‑Entry Accounting, Financial Period Management  
- Trial Balance, P&L, Balance Sheet, Cash Flow, Bank Reconciliation  

### Sales Management
- Customer Management, Quotations, Orders, Delivery Notes  
- Invoices, Credit Notes, Customer Payments  

### Purchasing
- Supplier Management, Requisitions, Purchase Orders  
- Goods Receipt Notes (GRN), Invoices, Debit Notes, Supplier Payments  

### Inventory Management
- Product & Warehouse Management  
- Stock Movement, Adjustments, Transfers, Valuation  

### Tax Management
- GST, SST, Input/Output Tax, Tax Codes  
- Tax Reporting & Audit Support  

### User & Security
- Authentication & Authorization  
- JWT + Refresh Tokens  
- Role‑Based Access Control (RBAC)  
- Multi‑Company Access & Permissions  

### Reporting
- Financial, Sales, Purchase, Inventory, Tax, and Audit Reports  

---

## 🏢 Business Workflows

### Purchase Cycle
```
Purchase Requisition → Purchase Order → GRN → Purchase Invoice → Supplier Payment
```

### Sales Cycle
```
Quotation → Sales Order → Delivery Order → Sales Invoice → Customer Payment
```

---

## 💰 Accounting Workflow

PrimeLedger enforces **double‑entry accounting** to ensure balanced transactions:

**Purchase Invoice**
```
Inventory                Dr
GST Input Tax            Dr
      Accounts Payable               Cr
```

**Sales Invoice**
```
Accounts Receivable      Dr
      Sales Revenue                  Cr
      GST Output Tax                 Cr
```

---

## 🔍 Audit‑Friendly Design

- Complete Audit Trail  
- Soft Delete & Transaction History  
- Created/Updated Metadata  
- Approval Workflow  
- Immutable Financial Records  
- Document Lifecycle Tracking  

---

## 🏢 Multi‑Tenant Architecture

**Database‑per‑Company** design ensures tenant isolation, security, and scalability:

```
Tenant A → Database A
Tenant B → Database B
Tenant C → Database C
```

Benefits: independent maintenance, simplified backup/restore, horizontal scalability.

---

## 🗄 Database Principles

- Third Normal Form (3NF)  
- Foreign Key Constraints & Transactions  
- Optimized Indexes, Stored Procedures, Views  
- Query Optimization & Concurrency Control  

---

## ⚙ Technology Stack

**Backend**: ASP.NET Core, C#, EF Core, RESTful APIs  
**Frontend**: React, TypeScript, Tailwind CSS  
**Database**: Microsoft SQL Server  
**Authentication**: JWT, Refresh Tokens, RBAC  
**DevOps (Planned)**: Docker, GitHub Actions, CI/CD, Google Cloud, Kubernetes  

---

## 🏛 Architecture

Layered design for clarity and scalability:

```
Presentation Layer → Application Layer → Domain Layer → Infrastructure Layer → Persistence Layer → SQL Server
```

Future enhancements: Clean Architecture, CQRS, MediatR, Event‑Driven Design, Redis, RabbitMQ.

---

## 🚀 Roadmap

- Dashboard Analytics  
- Bank Reconciliation, Budgeting, Fixed Assets  
- Manufacturing, Payroll, HR Module  
- API Gateway, Redis, RabbitMQ  
- Docker, Kubernetes, Google Cloud Deployment  

---

## 💡 Why PrimeLedger?

PrimeLedger is more than a project—it’s a **portfolio of enterprise engineering practices**. It demonstrates how ERP systems are designed for **financial compliance, scalability, and auditability**, while showcasing modern software architecture principles.

---

## 👨‍💻 Author

**Agaesh Kumar N Senturvasan**  
Software Engineer • Backend Developer • ERP & Financial Systems Enthusiast  

- LinkedIn: *(Add your profile)*  
- GitHub: *(Add your profile)*  

---

## 📄 License

Licensed under the MIT License.
