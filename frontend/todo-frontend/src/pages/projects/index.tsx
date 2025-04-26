import LeftMenu from "../../components/left-menu";


const ProjectsPage = () => {
    return (
        <div className="dashboard">
            <LeftMenu />
            <main className="dashboard__main">
                <header className="dashboard__header">
                    <div className="dashboard__header-left">
                        <h1 className="dashboard__title">Projects</h1>
                    </div>
                    <div className="dashboard__header-actions">
                        <button className="dashboard__btn dashboard__btn--primary">Create New</button>
                    </div>
                </header>
                <section className="dashboard__widgets">
                    <div className="widget">
                        <div className="widget__info">
                            <p className="widget__label widget__header">Active Task</p>
                            <p className="widget__label">Task that is still ongoing</p>
                            <button className="dashboard__btn dashboard__btn--ghost" style={{ marginTop: 20}}>View details</button>
                        </div>
                    </div>
                    <div className="widget widget--with-image">
                        <div className="widget__info">
                            <p className="widget__label widget__header">Client's Review</p>
                            <p className="widget__label">Task that is on client's review</p>
                            <button className="dashboard__btn dashboard__btn--ghost" style={{ marginTop: 20}}>View details</button>
                        </div>
                    </div>
                    <div className="widget widget--with-image">
                        <div className="widget__info">
                            <p className="widget__label widget__header">Completed Task</p>
                            <p className="widget__label">Task that approved by client</p>
                            <button className="dashboard__btn dashboard__btn--ghost" style={{ marginTop: 20}}>View details</button>
                        </div>
                    </div>
                </section>
            </main>
        </div>
    );
};

export default ProjectsPage;