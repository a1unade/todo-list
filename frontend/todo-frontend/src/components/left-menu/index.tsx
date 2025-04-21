import logo from "../../assets/images/logo.svg";
import {FolderIcon, HomeIcon, TasksIcon} from "../../assets/icons";

const LeftMenu = () => {
    return (
        <aside className="sidebar">
            <div className="sidebar-header">
                <div className="sidebar-header__naming">
                    <img src={logo} alt="logo" className="sidebar-header__logo"/>
                    <p>Project</p>
                </div>
            </div>
            <div className="sidebar-section">
                <button className="sidebar-section__button">
                    <HomeIcon />
                    <span>Dashboard</span>
                </button>
                <button className="sidebar-section__button">
                    <TasksIcon />
                    <span>My Tasks</span>
                </button>
            </div>
            <div className="sidebar-section" style={{ marginTop: "10px" }}>
                <div className="sidebar-section__header">
                    <p>WORKSPACE</p>
                </div>
                <button className="sidebar-section__button">
                    <div className="sidebar-section__icons_container">
                        <FolderIcon />
                    </div>
                    <span>my-first-project</span>
                </button>
                <button className="sidebar-section__button">
                    <div className="sidebar-section__icons_container">
                        <FolderIcon />
                    </div>
                    <span>my-second-project</span>
                </button>
            </div>
        </aside>
    );
};

export default LeftMenu;