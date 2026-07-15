import './Sidebar.css';
import { Link } from 'react-router-dom';
import { LogOut, ChevronDown, ChevronRight } from 'lucide-react';
import { useState } from 'react'; 
import sidebarItems from './SidebarItems';
import SidebarLogo from './PrimeLedger.png'
function Sidebar() {
    // Track which parent is open
    const [activeItem, setActiveItem] = useState(null);

    return (
        <div className="sidebar">
            {/* Logo Area */}
            <div className="sidebar-logo">

                <img src={SidebarLogo} alt="Sidebar Logo" />
            </div>

            {/* Navigation */}
            <nav className="sidebar-nav">
                {sidebarItems.map((item) => {
                    const Icon = item.icon;
                    const isOpen = activeItem === item.title;

                    return (
                        <div key={item.title}>
                            {item.link ? (
                                <Link to={item.link} className="sidebar-nav-item">
                                    <Icon size={20} strokeWidth={2.2} />
                                    <span>{item.title}</span>
                                </Link>
                            ) : (
                                <>
                                    <div
                                        className={`sidebar-nav-parent ${isOpen ? "active" : ""}`}
                                        onClick={() => setActiveItem(isOpen ? null : item.title)}
                                        style={{
                                            cursor: "pointer",
                                            display: "flex",
                                            alignItems: "center",
                                            gap: "8px",
                                        }}
                                    >
                                        <Icon size={20} strokeWidth={2.2} />
                                        <span>{item.title}</span>
                                    <div className="sidebar-nav-parent-icon" style={{ marginLeft: "auto" }}>
                                        {isOpen ? (
                                                    <ChevronDown size={16} strokeWidth={2} />
                                                ) : (
                                                    <ChevronRight size={16} strokeWidth={2} />
                                                )}
                                            </div>
                                    </div>

                                        {isOpen && (
                                            <div className={`sidebar-nav-submenu ${isOpen ? " open" : ""}`}>
                                    {item.children?.map((child) => {
                                        const ChildIcon = child.icon;
                                        return (
                                            <Link
                                                key={child.title}
                                                to={child.link}
                                                className="sidebar-nav-item child"
                                            >
                                                <ChildIcon size={18} strokeWidth={2} />
                                                <span>{child.title}</span>
                                            </Link>
                                        );
                                    })}
                                        </div>
                                    )}
                                </>
                            )}
                        </div>
                    );
                })}
            </nav>

            {/* User Profile Bottom */}
            <div className="sidebar-footer">
                <div className="sidebar-profile">
                    <img
                        src="https://ui-avatars.com/api/?name=Alex+Doe&background=f3f4f6&color=111827"
                        alt="User"
                        className="sidebar-profile-image"
                    />
                    <div className="sidebar-profile-info">
                        <p className="sidebar-profile-name">Alex Doe</p>
                        <p className="sidebar-profile-role">Finance Admin</p>
                    </div>
                    <LogOut size={30} strokeWidth={2} className="sidebar-profile-arrow" />
                </div>
            </div>
        </div>
    );
}

export default Sidebar;