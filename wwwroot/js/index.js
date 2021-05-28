window.addEventListener('load',function(){
    var welcome = document.querySelector('.greet'),
        subtext = document.querySelector('.subTexts'),
        form    = document.querySelector('.sub'),
        follow  = document.querySelector('.followUs'),
        delay = 1000;


    setTimeout(function(){welcome.style.top='0';},delay);
    setTimeout(function(){subtext.style.bottom = '0%';},delay*2);
    setTimeout(function(){form.style.opacity='1';},delay*5);
    setTimeout(function(){follow.style.bottom='0%';},delay*6);
});
