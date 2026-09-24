import { apiFetch } from "./api-client";
import type { Todo } from "./types";

export async function getTodos(): Promise<Todo[]> {
	const res = await apiFetch<Todo[]>("/todoitems");

	return res;
}

export async function toggleStatus(id: string): Promise<Todo> {
	return await apiFetch<Todo>(`/todoitems/toggle-status/${id}`, {
		method: "PUT",
	});
}
