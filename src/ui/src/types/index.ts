export type Priority = 'None' | 'Low' | 'Medium' | 'High';

export const PRIORITIES: Priority[] = ['None', 'Low', 'Medium', 'High'];

export interface Todo {
  id: string | number;
  description: string;
  completedAt: string | null;
  isCompleted: boolean;
  priority: Priority;
  dueDate: string | null;
  creationTime: string;
  lastModifiedTime: string;
  rowVersion: string;
  error?: string | null;
}

export type CreateTodoDto = Pick<Todo, 'description'>;

export interface PaginatedResponse<T> {
  items: T[];
  totalItems: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export type Filter = 'active' | 'archived';
