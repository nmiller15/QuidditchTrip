import { get, post } from './request.js';

export default {
    quaffle: async function(gameKey) {
        var team = await this.getActiveTeam(gameKey);
        var teamKey = team.teamKey;
        return await this.score(teamKey, 10);
    },
    bludger: async function(gameKey) {
        var team = await this.getActiveTeam(gameKey);
        var teamKey = team.teamKey;
        return await this.score(teamKey, -10);
    },
    snitch: async function(gameKey) {
        var success = confirm("Are you sure you want to score the snitch? This will end the game.");
        if (!success) { return; } 

        var team = await this.getActiveTeam(gameKey);
        var teamKey = team.teamKey;
        const finalScore = await this.score(teamKey, 150);
        window.location.href = `/final?gameKey=${gameKey}`
    }, 
    changeActiveTeam: async function(gameKey) {
        var response = await post(`/games/${gameKey}/change-active/`);
        if (!response.wasSuccessful) { return alert("Issue changing active team! Try reloading!") }

        var teams = response.data.teams;
        const activeIndicator = 'text-red-600';
        for (let i = 0; i < teams.length; i++) {
            var name = document.querySelector(`div#team-${teams[i].teamKey} h3`);
            if (teams[i].isActive) { name.classList.add(activeIndicator); }
            else { name.classList.remove(activeIndicator); }
        }

        document.getElementById('toggle-timer').classList.remove('hidden');
        document.getElementById('change-team').classList.add('hidden');
    },
    getActiveTeam: async function(gameKey) {
        var response = await get('/games', [gameKey]);
        if (!response.wasSuccessful) { return alert("Error getting active team!"); }

        return response.data.teams[0].isActive 
            ? response.data.teams[0]
            : response.data.teams[1];
    },
    score: async function(teamKey, score) {
        var response = await post(`/teams/${teamKey}/score/${score}`);
        if (!response.wasSuccessful) { return alert("Error scoring!"); }
        this.updateScore(response.data, teamKey);
        return response.data;
    },
    updateScore: function(score, teamKey) {
        document.querySelector(`p#team-${teamKey}`).innerText = score;
    },
    end: async function(gameKey) {
        var response = await post(`/games/${gameKey}/end`);
        if (!response.wasSuccessful) { return alert("Error ending the game."); }
        return response.data;
    }
}
