import { FormControlLabel, Checkbox } from "@mui/material";

export default function SmallCheckbox({ label, checked, onChange }) {
    return (
        <FormControlLabel
            control={
                <Checkbox
                    checked={checked}
                    onChange={onChange}
                    sx={{
                        "& .MuiSvgIcon-root": {
                            fontSize: 15, // shrink checkbox icon
                        },
                    }}
                />
            }
            label={label}
            sx={{
                alignItems: "center",
                "& .MuiFormControlLabel-label": {
                    fontSize: 15,
                    fontWeight: 500,
                    color: "#555",
                    lineHeight: 1.2,
                    display: "flex",
                    alignItems: "center",
                },
            }}
        />
    );
}