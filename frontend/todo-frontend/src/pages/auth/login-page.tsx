import logo from "../../assets/images/logo.svg"

const LoginPage = () => {
    return (
        <div className="auth-layout">
            <div className="auth-card">
                <div className="auth-card__left">
                    <img className="auth__logo" src={logo} alt="logo" />
                    <h1 className="auth__project-name">Project</h1>
                </div>
                <div className="auth-card__right">
                    <h2 className="auth__title">Login</h2>
                    <p className="auth__subtitle">Welcome back! Please enter your credentials to access your account.</p>
                    <form className="auth__form">
                        <input type="email" className="auth__input" placeholder="Email" />
                        <input type="password" className="auth__input" placeholder="Password" />
                        <button type="submit" className="auth__button">Login</button>
                    </form>
                    <div className="auth__footer">
                        <span>Don't have an account?</span>
                        <a href="/register" className="auth__link">Sign up</a>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;