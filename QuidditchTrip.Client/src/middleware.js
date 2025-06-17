import { get } from './scripts/request.js';

export async function onRequest(context, next) {
	let { request, url } = context
	if (request.url.includes('.css') || request.url.includes('.js')) {
		return next()
	}

	const gameKey = url.searchParams.get('gameKey')
	if (!gameKey) {
		return next()
	}

        const game = await get('/games', [gameKey]);
	if (game.ok && game.wasSuccessful) {
		context.locals.game = await game.data.json()
	}
	return next()
}
