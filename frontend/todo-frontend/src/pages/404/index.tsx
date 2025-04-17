import logo from "../../assets/images/logo.svg";

const NotFoundPage = () => {
    return (
        <div className="notfound-layout">
            <div className="notfound-card">
                <div className="notfound-card__left">
                    <img className="notfound__logo" src={logo} alt="logo" />
                    <h1 className="notfound__project-name">Project</h1>
                </div>
                <div className="notfound-card__right">
                    <h1 className="notfound__title" style={{ fontSize: 100, marginBottom: 0 }}>404</h1>
                    <h2 className="notfound__title">Page not found</h2>
                    <p className="notfound__subtitle">The page you're looking for doesn't exist or has been moved.</p>
                    <button className="notfound__button">Go home</button>
                </div>
            </div>
        </div>
    );
};

export default NotFoundPage;
