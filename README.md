# CSS545 Checkpoint 1

### platform:

IOS

### framework/programming language:

Swift

### Whether you want to explore cross-platform development

yes, we want to do cross-platform development

### Include a brief explanation for your selections

We want to store user data in the cloud. Also, if our team wants to do a
game project, we want our battle process and data exchange in the
cloud.

# Checkpoint 1 - Project Proposal, initial design, and Challenges and Solutions

## Project Name: Hacking Game

Group members: Qingran Shao, Wentao Gao

Version Alpha

## Summary of project:

In our view, games are an exceedingly effective means of communication and entertainment. Chess and Go, through continuous competition, not only provides the joy of dopamine for the brain but also trains one's thinking. Video games break down the barriers of time and space that traditional games face. For example, Minecraft not only entertains but also encourages players to actively and proactively learn about logic circuits. The aim of the hacking game is to fill spare time while also fostering more interactions and connections among people, encouraging exchanges not just of languages but also of game activities. Additionally, in terms of market value, I hope to move away from traditional game monetization models that rely on loot boxes, purchasing in-game currency, and reducing game difficulty with cash. My revenue goal is to sell actual tools that enhance the playability of the game, such as cloud servers. In hacker duels, using tools, macros, or even AI pre-set in the servers to enhance gameplay can significantly elevate the commercial purpose and playability of the game。

## Project Analysis

### Value Proposition

Factually, there's a trend of development in games that resonate with my thoughts, with Pokémon Go being a prime example. Despite this, games like Pokémon Go and Ingress Prime often revolve around a singular, repetitive action pattern. This can lead to a decline in engagement among most players in the long run, especially when fresh content is lacking and there's no room for player creativity like in Minecraft.

### Primary Purpose

Our aspiration is to foster a game that not only motivates outdoor activities through mobile gaming but also possesses an extensive potential for gameplay evolution, including aspects like PvP and PvE confrontations. I believe that the incorporation of gamified hacking activities might be the ultimate resolution to enhance the gaming experience.

### Target Audience

Our targeted audience is people who wish to communicate with others on topics such as computer tech and network skills. We also encourage teenagers to play with code, mathematics, and hardware when they experience our game.

### Success Criteria

An event, a party that brings everyone together to play. To assess the success of this game, one should look beyond just profit and user numbers. We envision our game as a tool that enables quick socializing for players, even in unfamiliar public spaces. Here's an instance: in the current Chinese context of team-building work events and business negotiations, Honor of Kings – a mobile game akin to Dota – has taken over as the primary social instrument, surpassing traditional dinner gatherings or golf. In our view, if our game becomes one of the activity options at parties, then that will mark our success.

Success Criteria:
The true measure of our app's success isn't solely financial gain or the sheer number of users. It's about user satisfaction, market share, and the public good. We will evaluate our game's triumph through its ability to facilitate quick social interactions in any setting, its adoption as a preferred social tool in various gatherings, and whether it becomes a go-to choice for entertainment at parties. These indicators will tell us that our game has achieved its goal, beyond just the bottom line.

### Monetization Model

In-app purchases and Freemium.

### Competitor Analysis

The pros and cons list for Pokémon GO:

#### Advantages:

- Real-time GPS tracking that motivates people to step outside and socialize.
- The Pokémon IP has tremendous value and appeal.
- The gameplay is intuitive and easy to grasp.
- The game features high-quality models and visuals.

#### Disadvantages:

- The PvP (Player vs. Player) combat can be rather monotonous.
- The game tends to have a short lifespan for user engagement.
- The content of the game can become dull over time.

## Initial Design

This project introduces a multiplayer mobile game that simulates real-time hacking. In this game, participants assume the role of hackers aiming to compromise the virtual assets of their opponents. Upon entering specific real-world locations such as campuses, parks, or malls, players automatically become part of an ongoing game with others who have the app active.

Initially, players must conduct a scan to identify potential targets. This process comes in three modes: quick scan, deep scan, and area scan. The quick scan briefly surveys all players in the vicinity, providing a count and vague locations of some. The deep scan, although three times slower, offers the exact locations of all players in the area. The area scan, on the other hand, meticulously examines a smaller, designated zone for detailed information.

Once a target is chosen, the player can opt to either steal values from or sabotage the device of the selected opponent. To steal, the player must crack the target's protective lock within five seconds. Success means acquiring the target's virtual assets, while failure alerts the victim and initiates a hacking duel. Opting for sabotage directly leads to a hacking confrontation, with the aim of overwhelming the victim's defenses to plunder their assets.

### UI/UX Design

![Initial Design](./HackGame.png "Initial Design")

### Technical Architecture

![Technical Architecture](./Group%201.png)

## Challenges and Open Questions

### Form factor

We will use iPhone 13 as the standard Test platform.

### Restoration Of Data on Reinstall

User Account (Username and Password) will be stored online, e.g. AWS.
User Generate-Content (Skill, Purchases, and Level) will be stored online.

### GPS sensors

Unity may not invoke iOS Devices' GPS sensors, so that is the reason we may use Xcode and Unity together.

### Interaction With AWS

We have little experience with AWS, so we may meet many issues with we deploy a response server on AWS.

# Initial Mockup

![Initial Mockup](./Frame%2031.png "Initial Mockup")

### Happy Path

For our game, the happy path would be:

Open the app --> enter user credential --> click login -->
click one of the scan button --> click one of the action button -->
enter one of the battle page --> successfully solve the question -->
evaluation page to collect points --> back to world to look for more battles

### Success Criteria

For a mobile game, the success criteria will be points earned. The more a player's points are would mean
that the player has sucessfully played a lot of rounds of the game.
For a mobile game, the success criteria will be points earned. The more a player's points are would mean
that the player has sucessfully played a lot of rounds of the game.

---

- a display of the map, in here we used Google Map API
- a point to display player location, we had to ask for user permission and get longitude and latitude to put into the map location
- a UI to show actions player can take, we used unity buttons
- multiple points to display enemy location after scan action taken, which requires phone to request server to send other player info. In the MVP here we used dummy locations
- a way to interact with enemy shown, in here we used on tap to bring out pop up menu
- a pop up menu to display actions player can take to enemy after tapping enemy
- a page displaying the result of the action
- a way to store the game points and resources points for the user
- a way to close the page and go back to the map

### Mapping between features and value(s) to be delivered by your app (justification)

For each of the features listed:

- Map display, by using Google Map API: enhances user experience by providing a familiar and interactive map interface. It helps users orient themselves in the game environment, linking virtual elements with real-world locations.
- Player Location Indication: increases realism and personalization by showing the player's actual location. This feature supports navigation and strategy, making the game experience more engaging and relevant to the user's real-world surroundings.
- User interface with action buttons: simplifies gameplay by providing easy access to game functions such as the quick scan. This feature makes the game more user-friendly and accessible.
- Enemy Detection such as Quick Scan and interacting with enemy: can add excitement and a sense of unpredictability. This feature encourages exploration and interaction within the game.
- Action pop up menu: enhances user engagement by offering choices such as gathering resources or attacking enemies. As we add more ways for player to intereact with enemy, it would allow for a more diverse gameplay strategies and increases the game's replay value.
- Result and score display page: Provides immediate feedback on player actions, contributing to a sense of accomplishment and progress. It helps players track their performance.
- Game data storage and way to update: Ensures continuity and progression in the game.

### updates

- mini games
- user profile
- account system

### State Management

unity provide MonoBehaviour. MonoBehavior has default OnApplicationPause, and OnApplicationFocus states. When Run In Background (Edit > Project Settings > Player > Resolution and Presentation) is disabled, a game running in the Editor's Play mode or in a standalone Player will pause any time the Editor or Player application loses focus. In these cases Unity sends OnApplicationPause(true) to all MonoBehaviours.

The pauseStatus parameter is either true (paused) or false (running). All MonoBehaviours receive this event while they are initializing, just after Awake, so it will be called (with status false) on first entering Play mode. They receive it again whenever the application pauses or unpauses on losing or regaining focus.

Note: Unity does not call OnApplicationPause in response to toggling the Pause button in the Editor toolbar. The status of the pause button in the Editor is tracked by the PauseState enum.

For OnApplicationPause to trigger in a Player application running separately from the Editor, the running Player must be windowed and smaller than the full screen. If the game is hidden (fully or partly) by another application then it pauses and Unity calls OnApplicationPause with true. When the game regains focus, Unity calls OnApplicationPause with false.

OnApplicationPause can be a co-routine; to do this use the yield statement in the function. Implemented this way, it is evaluated twice during the initial frame: first as an early notification, and secondly during the normal co-routine update step.

On Android, enabling the on-screen keyboard causes an OnApplicationFocus event with the value false. However, if you press "Home" at the moment the keyboard is enabled, the OnApplicationFocus event is not called and OnApplicationPause is called instead.

In this case, our user data will be store in Unity allocated memory and restore when application awake. In case avoid to missing data in local when application paused, we will upload data to could when the application run in background.
