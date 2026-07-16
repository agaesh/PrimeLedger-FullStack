import { Pagination } from "@mui/material";
import "./Pagination.css";

const PaginationComponent = ({ totalPages, currentPage, onChange }) => {
    return (
        <div className="pagination-container">
            <Pagination
                count={totalPages}
                page={currentPage}
                onChange={(event, page) => onChange(page)}
                color="primary"
                shape="rounded"
                siblingCount={1}
                boundaryCount={1}
            />
        </div>
    );
};

export default PaginationComponent;