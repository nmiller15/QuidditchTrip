import { log } from './log.js'

export async function request(path, method, params = []) {request
	const baseUrl = 'https://api-quidditchtrip.nolanmiller.me'
	let url = `${baseUrl}${path}`

	for (let i = 0; i < params.length; i++) {
		if (params[i]) {
			url = `${url}/${params[i]}`
		}
	}

	const options = {
		method: method
	}

        log(`${method} request issued to ${url}`);

	const response = await fetch(url, options)
	if (!response.ok) {
		throw new Error(`HTTP error! status: ${response.status}`)
	}
	const data = await response.json()
	return data
}

export async function get(path, params) {
	return await request(path, 'GET', params)
}

export async function post(path, params) {
	return await request(path, 'POST', params)
}
