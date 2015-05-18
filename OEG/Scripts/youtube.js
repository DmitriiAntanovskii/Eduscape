    var tag = document.createElement('script');
tag.src = "https://www.youtube.com/player_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
var player;

function onYouTubePlayerAPIReady() {
    player = new YT.Player('player', {
        height: '272',
        width: '450',
        videoId: 'F6GY_OYstR0',
        playerVars: { rel: 0, autoplay: 1 },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
}

function onPlayerReady(event) {
    /// event.target.playVideo();
}

function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.PLAYING) {
        _gaq.push(['_trackEvent', 'Video', 'Play',
        player.getVideoUrl()]);
    }
    if (event.data == YT.PlayerState.PAUSED) {
        _gaq.push(['_trackEvent', 'Video', 'Paused',
        player.getVideoUrl()]);
    }
    if (event.data == YT.PlayerState.ENDED) {
        $("#play").show();
        $("#player").hide();
        _gaq.push(['_trackEvent', 'Video', 'Watch to End',
        player.getVideoUrl()]);
    }
}
// ]]>
