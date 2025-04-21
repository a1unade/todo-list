import LeftMenu from "../../components/left-menu";

const Home = () => {
    return (
        <div className="dashboard">
            <LeftMenu />
            <main className="dashboard__main">
                <header className="dashboard__header">
                    <div className="dashboard__header-left">
                        <h1 className="dashboard__title">Good morning, Axel!</h1>
                        <p className="dashboard__subtitle">Short description will be placed right over here</p>
                    </div>
                    <div className="dashboard__header-actions">
                        <button className="dashboard__btn dashboard__btn--ghost">Customize</button>
                        <button className="dashboard__btn dashboard__btn--primary">+ Create New</button>
                    </div>
                </header>

                <section className="dashboard__widgets">
                    <div className="widget widget--with-image">
                        <div className="widget__image" />
                        <div className="widget__info">
                            <h2 className="widget__value">72</h2>
                            <p className="widget__label">Active Task</p>
                        </div>
                    </div>
                    <div className="widget widget--with-image">
                        <div className="widget__image" />
                        <div className="widget__info">
                            <h2 className="widget__value">21</h2>
                            <p className="widget__label">Client Review</p>
                        </div>
                    </div>
                    <div className="widget widget--with-image">
                        <div className="widget__image" />
                        <div className="widget__info">
                            <h2 className="widget__value">51</h2>
                            <p className="widget__label">Completed Task</p>
                        </div>
                    </div>
                </section>

                <section className="dashboard__bottom">
                    <div className="dashboard__recent">
                        <h3 className="dashboard__section-title">Recently Added</h3>
                        <ul className="dashboard__list">
                            <li className="dashboard__list-item">Marketing Site</li>
                            <li className="dashboard__list-item">Design new pricing page</li>
                            <li className="dashboard__list-item">Presenting three design concepts</li>
                            <li className="dashboard__list-item">Sprint #1 | Finalizing Home Page</li>
                            <li className="dashboard__list-item">Documenting Style Guide</li>
                        </ul>
                    </div>
                    <div className="dashboard__chart">
                        <h3 className="dashboard__section-title">Task Completion Rate</h3>
                        <div className="dashboard__chart-placeholder">Chart Placeholder</div>
                    </div>
                </section>
            </main>
        </div>
    );
};

export default Home;