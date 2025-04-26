import React, { useEffect, useState } from 'react';
import {Project, Task} from "../../types/project/project-types.ts";
import apiClient from "../../utils/api-client/api-client.ts";
import CreateProjectModal from "../../components/modals/create-project-modal.tsx";
import AddTaskModal from "../../components/modals/add-task-modal.tsx";
import LeftMenu from "../../components/left-menu";

const Home: React.FC = () => {
    const [projects, setProjects] = useState<Project[]>([]);
    const [selectedProject, setSelectedProject] = useState<Project | null>(null);
    const [isProjectModalOpen, setProjectModalOpen] = useState<boolean>(false);
    const [isTaskModalOpen, setTaskModalOpen] = useState<boolean>(false);

    useEffect(() => {
        apiClient.get<Project[]>('/api/projects').then(res => setProjects(res.data));
    }, []);

    const handleProjectCreated = (project: Project) => {
        setProjects(prev => [...prev, project]);
        setSelectedProject(project);
    };

    const handleTaskAdded = (task: Task) => {
        console.log('Task added', task);
    };

    return (
        <div className="dashboard">
            <LeftMenu />
            <main className="dashboard__main">
                <header className="dashboard__header">
                    <div className="dashboard__header-left">
                        <h1 className="dashboard__title">Good morning!</h1>
                        <p className="dashboard__subtitle">Your project overview</p>
                    </div>
                    <div className="dashboard__header-actions">
                        <button
                            className="dashboard__btn dashboard__btn--ghost"
                            onClick={() => setProjectModalOpen(true)}
                        >Create Project</button>
                        <button
                            className="dashboard__btn dashboard__btn--primary"
                            disabled={!selectedProject}
                            onClick={() => setTaskModalOpen(true)}
                        >Add Task</button>
                    </div>
                </header>

                <CreateProjectModal
                    isOpen={isProjectModalOpen}
                    onClose={() => setProjectModalOpen(false)}
                    onCreated={handleProjectCreated}
                />
                <AddTaskModal
                    isOpen={isTaskModalOpen}
                    onClose={() => setTaskModalOpen(false)}
                    projectId={selectedProject?.id ?? 0}
                    onAdded={handleTaskAdded}
                />

                <section className="dashboard__recent">
                    <h3 className="dashboard__section-title">Projects</h3>
                    <ul className="dashboard__list">
                        {projects.map(proj => (
                            <li
                                key={proj.id}
                                className={`dashboard__list-item ${selectedProject?.id === proj.id ? 'selected' : ''}`}
                                onClick={() => setSelectedProject(proj)}
                            >
                                {proj.name}
                            </li>
                        ))}
                    </ul>
                </section>
            </main>
        </div>
    );
};

export default Home;