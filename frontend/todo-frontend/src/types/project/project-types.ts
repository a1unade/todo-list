export interface Project {
    id: number;
    name: string;
    description?: string;
}

export interface Task {
    id: number;
    title: string;
    description?: string;
    projectId: number;
}