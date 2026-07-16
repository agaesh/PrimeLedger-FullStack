import { useState, useEffect } from "react";
import {
    Paper,
    Divider,
    Box,
    Typography,
    TableContainer,
    Button
} from "@mui/material";
import FilterAccounts from './FilterAccounts';
import AccountsTable from './AccountsTable';
import { getChartsOfAccounts, deleteChartOfAccount } from "../../API/ChartsOfAccount.js";

function ChartsOfAccounts() {
    const [accounts, setAccounts] = useState([]);

    const [search, setSearch] = useState("");
    const [filterStatus, setFilterStatus] = useState("All");
    const [filterType, setFilterType] = useState("All");

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    // const [selectedRow, setSelectedRow] = useState(null);

    // const [setForm] = useState({
    //     code: "",
    //     name: "",
    //     type: "",
    //     balance: "",
    //     parent: "",
    //     posting: "",
    //     status: "",
    // });

    useEffect(() => {
        const fetchAccounts = async () => {
            try {
                const data = await getChartsOfAccounts();
                setAccounts(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchAccounts();
    }, []);

    // Assuming you already have deleteChartOfAccount(id) defined
    const handleRowDelete = async (id) => {
        try {
            // Call your API helper to delete
            await deleteChartOfAccount(id);

            // Update local state by removing the deleted account
            setAccounts((prevAccounts) =>
                prevAccounts.filter((account) => account.id !== id)
            );

            console.log(`Account with id ${id} deleted successfully`);
        } catch (error) {
            console.error("Error deleting account:", error);
        }
    };

    

    return (
        <div style={{ padding: "20px" }}>
            <Typography variant="h5">
                Charts Of Accounts
            </Typography>

            <Typography variant="caption" color="textSecondary">
                Centralize and control your financial accounts.
            </Typography>

            {/* Search & Filters */}

            <Box
                sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center", // keeps children aligned vertically
                    gap: 2,                // small spacing between items
                }}
            >
                <FilterAccounts
                    filterType={filterType}
                    setFilterType={setFilterType}
                    filterStatus={filterStatus}
                    setFilterStatus={setFilterStatus}
                    search={search}
                    setSearch={setSearch}
                />

                <Box display="flex" flexDirection="column" gap={1}>
                    <Button variant="contained" color="primary" size="small" sx={{ fontSize: "12px" }}>
                        Create Account
                    </Button>
                    <Button variant="outlined" color="secondary" size="small" sx={{ fontSize: "12px" }}>
                        Import
                    </Button>
                    <Button variant="outlined" color="secondary" size="small" sx={{ fontSize: "12px" }}>
                        Export
                    </Button>
                    <Button variant="text" color="error" size="small" sx={{ fontSize: "12px" }}>
                        Clear Filters
                    </Button>
                </Box>
            </Box>

            <TableContainer component={Paper}>
                <Box p={2}>
                    {/* Table */}
                    <AccountsTable
                        accounts={accounts}
                        isloading={loading}
                        error={error}
                        handleRowDelete={handleRowDelete}
                    />
                </Box>
            </TableContainer>

            <Divider sx={{ my: 3 }} />

            {/* Form section can be added here later */}
        </div>
    );
}

export default ChartsOfAccounts;
