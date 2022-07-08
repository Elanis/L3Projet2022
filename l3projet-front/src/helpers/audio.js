function playSound(url, volume) {
	const audio = new Audio(url);
	audio.volume = volume || 0.2;
	audio.play();
}

export const error = () => playSound('/snd/error.ogg');
export const select = () => playSound('/snd/select_008.ogg');