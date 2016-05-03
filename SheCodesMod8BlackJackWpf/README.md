The 52 card face images used in this project are free for personal use. Credit goes to http://www.jfitz.com/cards/

As with the other parts of this repo. I tried to only use the tools that had been covered in Bob Tabor's course up to the 
point of the corresponding module in the shecodes companion. The purpose behind this was to avoid using concepts that I am wholly unfamiliar with, 
yet can be copied and pasted from sites like stackoverflow.

With BlackJack Wpf I strayed from this a couple of times and it promptly produced some bugs in edge cases/certain situations of the game:
I used  await Task.Delay and in some cases cards will get displayed / moves will be made after the game is over.
Can probably be fixed with a cancellation token, but I need to learn the concept behind async first. I decided to use it to be able to create a 
semblance of animation (i.e. cards get displayed with a slight delay, cardbacks are shown first, then card is "turned around")...



