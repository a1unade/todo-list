import logo from "../../assets/images/logo.svg"

const RegisterPage = () => {
    return (
        <div className="auth-layout">
            <div className="auth-card">
                <div className="auth-card__left">
                    <img className="auth__logo" src={logo} alt="logo" />
                    <h1 className="auth__project-name">Project</h1>
                </div>
                <div className="auth-card__right">
                    <h2 className="auth__title">Create Account</h2>
                    <form className="auth__form">
                        <input className="auth__input" type="text" placeholder="Name" />
                        <input className="auth__input" type="text" placeholder="Surname" />
                        <input className="auth__input" type="text" placeholder="Nickname" />
                        <input className="auth__input" type="date" placeholder="Birth Date" />
                        <select className="auth__input">
                            <option value="">Gender</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                            <option value="Other">Other</option>
                        </select>
                        <input className="auth__input" type="text" placeholder="Country" />
                        <button className="auth__button">Register</button>
                    </form>
                    <div className="auth__footer">
                        <span>Already have an account?</span>
                        <a href="/login" className="auth__link">Log in</a>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RegisterPage;