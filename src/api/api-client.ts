export async function apiFetch<T>(
	path: string,
	init: RequestInit = {},
): Promise<T> {
	const res = await fetch(`http://localhost:5064/api${path}`, {
		...init,
		headers: {
			"Content-Type": "application/json",
			Accept: "application/json",
			...init.headers,
		},
	});

	if (!res.ok) {
		throw new Error(`API Error: ${res.status} ${res.statusText}`);
	}

	return res.json() as Promise<T>;
}
