import score from './score.js';

export default {
    toTimeString: function(totalSeconds) {
        var minutes = Math.floor(totalSeconds / 60)
        var minutesString = minutes < 1 ? '0' : minutes.toString()
        var seconds = totalSeconds % 60
        var secondsString = seconds < 1 ? '00' : seconds < 10 ? `0${seconds}` : seconds.toString()
        return `${minutes}:${secondsString}`
    },
    startTimer: async function(gameKey) {
        var affirmative = confirm("Click \"Okay\" to start the timer.");
        if (!affirmative) {
            console.log('Canceled');
            document.getElementById('timer').dataset.active = '0';
            return; 
        }
        
        var toggleButton = document.getElementById('toggle-timer');
        toggleButton.innerText = 'Stop Timer';

        var scoreButtons = document.getElementById('score-buttons');
        scoreButtons.classList.remove('hidden');
        
        var timer = document.getElementById('timer')
        var seconds = Number(timer.dataset.seconds)
        let interval = setInterval(async () => {
            console.log('tick');
          if (timer.dataset.active == "0" || seconds <= 0) {
            if (seconds <= 0) { 
                timer.dataset.active = '0'; 
                timer.dataset.seconds = '300';
                timer.innerText = this.toTimeString(300);
                toggleButton.innerText = 'Start Timer';
                toggleButton.classList.add('hidden');
                scoreButtons.classList.add('hidden');
                document.getElementById('change-team').classList.remove('hidden');
            }
            clearInterval(interval);
            return;
          }

          seconds--;
          timer.innerText = this.toTimeString(seconds);
          timer.dataset.seconds = seconds.toString();
        }, 1000);
    },
    stopTimer: function() {
        var toggleButton = document.getElementById('toggle-timer');
        var scoreButtons = document.getElementById('score-buttons');

        toggleButton.innerText = 'Start Timer';
        scoreButtons.classList.add('hidden');

        var timer = document.getElementById('timer')
        timer.dataset.active = '0'
    }
}
