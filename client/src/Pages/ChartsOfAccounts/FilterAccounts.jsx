import {
    Box,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    TextField,
    Typography,
    FormGroup,
    Divider,
} from "@mui/material";
import SmallCheckBox from "../../Components/Inputs/SmallCheckBox";

export default function FilterAccounts({
    filterType,
    setFilterType,
    filterStatus,
    setFilterStatus,
    search,
    setSearch,
    filterDebit,
    setFilterDebit,
    filterCredit,
    setFilterCredit,
    filterHierarchyParent,
    setFilterHierarchyParent,
    filterHierarchyChild,
    setFilterHierarchyChild,
    filterHierarchyBoth,
    setFilterHierarchyBoth,
    filterPostable,
    setFilterPostable,
    filterUnPostable,
    setFilterUnPostable,
}) {
    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 3 }}>
            {/* Top Row: Search + Dropdowns */}
            <Divider/>
            <Typography variant="body1" fontWeight="500" gutterBottom color="textPrimary">
                Filters By
            </Typography>
            <Box
                sx={{
                    display: "flex",
                    alignItems: "center",
                    gap: 3,
                    flexWrap: "wrap",
                }}
            >
                {/* Search */}
                <TextField
                    label="Search Accounts"
                    variant="outlined"
                    size="small"
                    value={search}
                    onChange={setSearch}
                    sx={{
                        width: 240,
                        "& .MuiOutlinedInput-root": { height: 40, fontSize: 14 },
                        "& .MuiInputLabel-root": { fontSize: 13 },
                    }}
                />

                {/* Account Type */}
                <FormControl
                    size="small"
                    sx={{
                        minWidth: 180,
                        "& .MuiOutlinedInput-root": { height: 40, fontSize: 14 },
                        "& .MuiInputLabel-root": { fontSize: 13 },
                    }}
                >
                    <InputLabel>Account Type</InputLabel>
                    <Select
                        value={filterType}
                        label="Account Type"
                        onChange={(e) => setFilterType(e.target.value)}
                    >
                        <MenuItem value="All">All Types</MenuItem>
                        <MenuItem value="Asset">Asset</MenuItem>
                        <MenuItem value="Revenue">Revenue</MenuItem>
                        <MenuItem value="Liability">Liability</MenuItem>
                        <MenuItem value="Tax">Tax</MenuItem>
                    </Select>
                </FormControl>

                {/* Status */}
                <FormControl
                    size="small"
                    sx={{
                        minWidth: 160,
                        "& .MuiOutlinedInput-root": { height: 40, fontSize: 14 },
                        "& .MuiInputLabel-root": { fontSize: 13 },
                    }}
                >
                    <InputLabel>Status</InputLabel>
                    <Select
                        value={filterStatus}
                        label="Status"
                        onChange={(e) => setFilterStatus(e.target.value)}
                    >
                        <MenuItem value="All">All Status</MenuItem>
                        <MenuItem value="Active">Active</MenuItem>
                        <MenuItem value="Inactive">Inactive</MenuItem>
                        <MenuItem value="Deleted">Deleted</MenuItem>
                    </Select>
                </FormControl>
            </Box>

            <Divider />

            {/* Second Row: Checkbox Groups */}
            <Box sx={{ display: "none", alignItems: "flex-start", gap: 5 }}>
                {/* Transaction Type */}
                <Box>
                    <Typography variant="subtitle2" sx={{ mb: 1 }}>
                        Transaction Type
                    </Typography>
                    <FormGroup row>
                        <SmallCheckBox
                            label="Debit"
                            checked={filterDebit}
                            onChange={(e) => setFilterDebit(e.target.checked)}
                        />
                        <SmallCheckBox
                            label="Credit"
                            checked={filterCredit}
                            onChange={(e) => setFilterCredit(e.target.checked)}
                        />
                    </FormGroup>
                </Box>

                <Divider orientation="vertical" flexItem sx={{ mx: 2 }} />

                {/* Hierarchy */}
                <Box>
                    <Typography variant="subtitle2" sx={{ mb: 1 }}>
                        Hierarchy
                    </Typography>
                    <FormGroup row>
                        <SmallCheckBox
                            label="Parent"
                            checked={filterHierarchyParent}
                            onChange={(e) => setFilterHierarchyParent(e.target.checked)}
                        />
                        <SmallCheckBox
                            label="Child"
                            checked={filterHierarchyChild}
                            onChange={(e) => setFilterHierarchyChild(e.target.checked)}
                        />
                        <SmallCheckBox
                            label="Both"
                            checked={filterHierarchyBoth}
                            onChange={(e) => setFilterHierarchyBoth(e.target.checked)}
                        />
                    </FormGroup>
                </Box>

                <Divider orientation="vertical" flexItem sx={{ mx: 2 }} />

                {/* Postable */}
                <Box>
                    <Typography variant="subtitle2" sx={{ mb: 1 }}>
                        Postable
                    </Typography>
                    <FormGroup row>
                        <SmallCheckBox
                            label="Postable"
                            checked={filterPostable}
                            onChange={(e) => setFilterPostable(e.target.checked)}
                        />
                        <SmallCheckBox
                            label="Unpostable"
                            checked={filterUnPostable}
                            onChange={(e) => setFilterUnPostable(e.target.checked)}
                        />
                    </FormGroup>
                </Box>
            </Box>
        </Box>
    );
}
