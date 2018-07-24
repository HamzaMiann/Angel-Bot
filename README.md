# Angel-Bot
Discord chat (joke) bot with interesting features...

![view](https://thumbs.gfycat.com/EsteemedThoughtfulElephantbeetle-size_restricted.gif)

The current main implementation is done in C#. I will re-create the same implementation in both Python and Node.js projects.

### Commands:
> **God bless ([name]=optional)**

*Returns a blessing.*

> **God why**

*Returns a random message from the previous 20 recorded messages.*

> **God am ([question]=required)**

*Returns one of 3 responses to the question (random).*

> **God solve ([equation]=required)**

*Returns the answer of the solves math equation.*

> **God pray ([wish]=required)**

*Returns a random percentage chance that the wish will come true. Stores the chance by username for use by the "God status" command.*

> **God status**

*Returns the average percentage chance.*

> **God zone**

*Runs an async thread where the bot will reply "Don't let me into my zone" every 2 seconds until the "God stop" command is used.*

> **God stop**

*Stops the thread that is spawned from the "God zone" command.*
