import "./Dashboard.css";

function Dashboard() {
    return (
        <div className="dashboard">
            <div className="dashboard-header">
                <div>
                    <h1>Dashboard</h1>
                    <p>Welcome back! Here's an overview of your business.</p>
                </div>
            </div>

            <div className="quick-button-group">
                <button className="button blue">Create New Po</button>
                <button className="button green">Create New SO</button>
                <button className="button purple">Create New Invoice</button>
            </div>

            <div className="stats-grid">
                <div className="stat-card sales">
                    <span>Total Sales</span>
                    <h2>$45,820</h2>
                    <small>+12.5% this month</small>
                </div>

                <div className="stat-card invoice">
                    <span>Invoices</span>
                    <h2>328</h2>
                    <small>24 Pending</small>
                </div>

                <div className="stat-card customer">
                    <span>Customers</span>
                    <h2>1,284</h2>
                    <small>+56 new</small>
                </div>

                <div className="stat-card product">
                    <span>Products</span>
                    <h2>612</h2>
                    <small>18 Low Stock</small>
                </div>
            </div>

            <div className="dashboard-content">
                <div className="dashboard-panel">
                    <h3>Revenue Overview</h3>
                    <div className="panel-placeholder">
                        Chart goes here
                    </div>
                </div>

                <div className="dashboard-panel">
                    <h3>Recent Activities</h3>

                    <ul className="activity-list">
                        <li>Invoice INV-1001 created</li>
                        <li>Customer ABC Trading added</li>
                        <li>Purchase Order approved</li>
                        <li>Payment received from XYZ Sdn Bhd</li>
                        <li>Sales Order SO-203 completed</li>
                    </ul>
                </div>
            </div>
        </div>
    );
}

export default Dashboard;