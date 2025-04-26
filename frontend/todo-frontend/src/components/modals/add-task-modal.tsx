import React, { ChangeEvent, FormEvent, useState } from 'react';
import apiClient from "../../utils/api-client/api-client.ts";
import {Task} from "../../types/project/project-types.ts";

interface AddTaskModalProps {
    isOpen: boolean;
    onClose: () => void;
    projectId: number;
    onAdded: (task: Task) => void;
}

const AddTaskModal: React.FC<AddTaskModalProps> = ({ isOpen, onClose, projectId, onAdded }) => {
    const [title, setTitle] = useState<string>('');
    const [description, setDescription] = useState<string>('');

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const { data: task } = await apiClient.post<Task>(`/api/projects/${projectId}/tasks`, { title, description });
            onAdded(task);
            setTitle('');
            setDescription('');
            onClose();
        } catch (err) {
            console.error(err);
            alert('Error adding task');
        }
    };

    if (!isOpen) return null;
    return (
        <div className="modal-backdrop">
            <div className="modal">
                <h2>Add Task to Project</h2>
                <form onSubmit={handleSubmit} className="form">
                    <label>
                        Title:
                        <input
                            type="text"
                            value={title}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => setTitle(e.target.value)}
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
                        <button type="submit">Add</button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AddTaskModal;