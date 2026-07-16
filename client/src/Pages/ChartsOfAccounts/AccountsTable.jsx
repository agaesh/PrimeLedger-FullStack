import { useState } from "react"; 
import {
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody
} from "@mui/material";
import { X } from "lucide-react";

const AccountsTable = ({ accounts, loading, error, handleRowDelete }) => {
    const [selectedRow, setSelectedRow] = useState(null);
    const [setForm] = useState(null);

    return (
        <Table size="small">
            <TableHead>
                <TableRow sx={{ backgroundColor: "#f0f4f8" }}>
                    <TableCell sx={{ fontWeight: "bold" }}></TableCell>
                    <TableCell sx={{ fontWeight: "bold" }}>Code</TableCell>
                    <TableCell sx={{ fontWeight: "bold" }}>Name</TableCell>
                    <TableCell sx={{ fontWeight: "bold" }}>Type</TableCell>
                    <TableCell sx={{ fontWeight: "bold" }}>Normal Balance</TableCell>
                    <TableCell sx={{ fontWeight: "bold" }}>Allow Posting</TableCell>
                    <TableCell sx={{ fontWeight: "bold" }}>Status</TableCell>
                </TableRow>
            </TableHead>

            <TableBody>
                {loading ? (
                    <TableRow>
                        <TableCell colSpan={8} align="center">
                            Loading accounts...
                        </TableCell>
                    </TableRow>
                ) : error ? (
                    <TableRow>
                        <TableCell colSpan={8} align="center" sx={{ color: "red" }}>
                            Error: {error}
                        </TableCell>
                    </TableRow>
                ) : accounts.length === 0 ? (
                    <TableRow>
                        <TableCell colSpan={8} align="center">
                            No chart of accounts found.
                        </TableCell>
                    </TableRow>
                ) : (
                    accounts.map((acc, idx) => (
                        <TableRow
                            key={idx}
                            hover
                            selected={selectedRow === idx}
                            onClick={() => {
                                setSelectedRow(idx);
                                setForm(acc);
                            }}
                        >
                            <TableCell>
                                <X
                                    size={20}
                                    style={{ cursor: "pointer" }}
                                    onClick={(e) => {
                                        e.stopPropagation(); // prevent row selection when clicking delete
                                        handleRowDelete(acc.id);
                                    }}
                                />
                            </TableCell>
                            <TableCell>{acc.accountCode}</TableCell>
                            <TableCell>{acc.accountName}</TableCell>
                            <TableCell>{acc.accountType}</TableCell>
                            <TableCell>{acc.normalBalance}</TableCell>
                            <TableCell>{acc.allowPosting === 1 ? "Yes" : "No"}</TableCell>
                            <TableCell>{acc.is_active === 1 ? "Active" : "Inactive"}</TableCell>
                        </TableRow>
                    ))
                )}
            </TableBody>
        </Table>
    );
};

export default AccountsTable;
