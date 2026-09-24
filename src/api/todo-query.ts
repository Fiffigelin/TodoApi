import { apiFetch } from "./api-client";
import type { Todo } from "./types";

export async function getTodos(): Promise<Todo[]> {
	const res = await apiFetch<Todo[]>("/todoitems");

	console.log(res);
	return res;
}
