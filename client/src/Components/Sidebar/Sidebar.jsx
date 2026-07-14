import './Sidebar.css';
import { Link } from 'react-router-dom';
import { Landmark, LogOut, ChevronDown, ChevronRight } from 'lucide-react';
import { useState } from 'react'; 
import sidebarItems from './SidebarItems';
function Sidebar() {
    return (
        <div className="sidebar">

            {/* Logo Area */}
            <div className="sidebar-logo">
                <div className="sidebar-logo-icon">
                    <Landmark size={24} strokeWidth={2.2} />
                </div>

                <span className="sidebar-logo-text">
                    PrimeLedger
                </span>
            </div>

            {/* Navigation */}
            <nav className="sidebar-nav">

                {sidebarItems.map((item) => {
                    const Icon = item.icon;

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
                                        className="sidebar-nav-parent"
                                        onClick={() => setOpen(!open)}
                                        style={{ cursor: "pointer", display: "flex", alignItems: "center", gap: "8px" }}
                                    >
                                        <Icon size={20} strokeWidth={2.2} />
                                        <span>{item.title}</span>
                                            <div class="sidebar-nav-parent-icon" style={{ marginLeft: "auto" }}>
                                                {open ? (
                                                    <ChevronDown size={16} strokeWidth={2} />
                                                ) : (
                                                    <ChevronRight size={16} strokeWidth={2} />
                                                )}
                                            </div>
                                    </div>

                                    {open && (
                                        <div className="sidebar-nav-submenu">
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