import React, { ChangeEvent, FormEvent, useState } from 'react';
import apiClient from "../../utils/api-client/api-client.ts";
import {Project} from "../../types/project/project-types.ts";

interface CreateProjectModalProps {
    isOpen: boolean;
    onClose: () => void;
    onCreated: (project: Project) => void;
}

const CreateProjectModal: React.FC<CreateProjectModalProps> = ({ isOpen, onClose, onCreated }) => {
    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const { data: project } = await apiClient.post<Project>('/api/projects', { name, description });
            onCreated(project);
            setName('');
            setDescription('');
            onClose();
        } catch (err) {
            console.error(err);
            alert('Error creating project');
        }
    };

    if (!isOpen) return null;
    return (
        <div className="modal-backdrop">
            <div className="modal">
                <h2>Create New Project</h2>
                <form onSubmit={handleSubmit} className="form">
                    <label>
                        Name:
                        <input
                            type="text"
                            value={name}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => setName(e.target.value)}
                            required
                        />
                    </label>
                    <label>
                        Description:
                        <textarea
                            value={description}
                            onChange={(e: ChangeEvent<HTMLTextAreaElement>) => setDescription(e.target.value)}
                        />
                    </label>
                    <div className="form-actions">
                        <button type="button" onClick={onClose}>Cancel</button>
                        <button type="submit">Create</button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default CreateProjectModal;