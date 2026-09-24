import { apiFetch } from "./api-client";
import type { Todo, TodoDTO } from "./types";

export async function getTodos(): Promise<Todo[]> {
	const res = await apiFetch<Todo[]>("/todoitems");

	return res;
}

export async function toggleStatus(id: string): Promise<Todo> {
	return await apiFetch<Todo>(`/todoitems/toggle-status/${id}`, {
		method: "PUT",
	});
}

export async function putTodo(id: string, dto: TodoDTO): Promise<Todo> {
	return await apiFetch<Todo>(`/todoitems/${id}`, {
		method: "PUT",
		body: JSON.stringify(dto),
	});
}

export async function postTodo(dto: TodoDTO): Promise<Todo> {
	return await apiFetch<Todo>(`/todoitems`, {
		method: "POST",
		body: JSON.stringify(dto),
	});
}
