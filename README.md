# Social Pulse Insight Hub
## NHL Stenden | Final Assignment | Threading in C#

### Table of Content
- [1. Objective](#1-objective)
- [2. Components](#2-components)
  - [2.1. Unified Modeling Language (UML)](#21-unified-modeling-language-uml)
  - [2.2. Use Case](#22-use-case)
- [3. Define Features](#3-define-features)
  - [3.1 .NET MAUI](#31-.net-maui)
  - [3.2 PLINQ](#32-plinq)
- [4. Steps](#4-steps)
- [5. Timeline](#5-timeline)
- [6. User Stories](#6-user-stories)
- [7. Test Plan](#7-test-plan)
- [8. Additional Information](#8-additional-information)


### 1. Objective
Develop a C# .NET Maui application that allows users to analyze and visualize social media data from multiple platforms.
The application should use multi-threading, PLINQ, and provide a clean GUI for users to interact with the analytics dashboard.

### 2. Components
#### 2.1 Requirements
| Components | Description |
| --- | --- |
| C# .NET 8.0 or Higher | Utilize the latest version of C# .NET for application development. |
| Multi-Threading | Implement multi-threading techniques, such as PLINQ, to efficiently process and analyze social media data concurrently. |
| .NET Maui | Build the application using .NET Maui, ensuring cross-platform compatibility for both Android and iOS devices. |
| Clean GUI | Design an intuitive graphical user interface (GUI) for users to connect their social media accounts, view analytics, and customize data visualizations. |
| Version Control | Implement version control using Git to track changes, collaborate within the group, and ensure a smooth development process. |

#### 2.2 Functional
| Components | Description |
| --- | --- |
| Social Media Integration | Allow users to connect and authenticate with multiple social media platforms (e.g., Twitter, Facebook, Instagram). |
| Data Fetching | Fetch and process social media data using multiple asynchronized I/O calls to the respective APIs. |
| Analytics Dashboard | Display analytics such as post engagement, follower growth, and popular hashtags. Utilize PLINQ for efficient data processing. |
| Real-time Updates | Implement real-time updates for social media metrics using the thread pool, ensuring timely information for users. |
| Data Visualization | Use plots and graphs to visually represent social media analytics. Allow users to customize and interact with the visualizations. |

#### 2.3 Unified Modeling Language (UML)
(diagram)

#### 3.4 Use Case
(diagram)

### 3. Define Features
| Name | Version | Date | Note |
| --- | --- | --- | --- |
| Visual Studio 2022 | 17.8 | January 22, 2024 | The official source of the project. |
| .NET Maui | - | February 6, 2023 | .NET Multi-platform App UI development. | 
| .NET Framework | 8.0.201 | February 13, 2024| Language support: C# 12.0, Visual Basic 16.9 |
| GitHub | 3.11.4 | January 30, 2024 | Version Control for the project. |
| PLINQ | - | - | Identify areas for parallel processing in data fetching and analysis. |

#### 3.1 .NET MAUI
It provides a cross-platform framework for building native applications, and it comes with built-in support for creating visually appealing and interactive user interfaces, including data visualization components. It allows you to bind your data directly to the UI components. Ensure that your chosen visualization library supports data binding, enabling you to dynamically update visualizations based on real-time data from social media platforms. Consider the responsive design principles provided by .NET MAUI to ensure that your visualizations adapt well to different screen sizes and orientations across various devices.

#### 3.2 PLINQ
**Data Processing:** If your application involves processing large sets of social media data, PLINQ can help parallelize operations like filtering, sorting, and aggregating, improving overall performance.
**Analytics Dashboard:** When calculating analytics metrics such as post engagement, follower growth, or popular hashtags, PLINQ can be employed to parallelize computations and speed up the generation of insights.
**Real-Time Updates:** If your application includes real-time updates for social media metrics, PLINQ can be used to efficiently process incoming data from multiple sources concurrently.
**Data Visualization:** Depending on the complexity of your visualizations, PLINQ can aid in parallelizing data transformations before rendering graphs and charts.

### 4. Steps
| # | Title | Description |
| --- | --- | --- |
| 1 | Authentication | Set up authentication mechanisms for each social media platform. |
| 2 | Implement API calls | To fetch data from social media accounts. |
| 3 | Data | Handle data storage and update strategies. |
| 4 | Multi-Threading | Integrate PLINQ for efficient data processing. |
| 5 | Real-Time | Ensure timely and accurate information is displayed on the dashboard. |
| 6 | Data Visualization | Implement and interact visualizations for various analytics metrics. |
| 7 | GUI | Ensure a user-friendly experience with smooth navigation. |
| 8 | Testing | Test the application including unit- and perform integration testing. |
| 9 | Document | Create user documentation for future reference. |
| 10 | Submission | Prepare the final project submission, including the source code, documentation, and version control history. |
| 11 | Presentation | Prepare a presentation to showcase the key features, functionalities, and the development process. |

### 5. Timeline
| Phase | Date | Description |
| --- | --- | --- |
| System Architecture | March 1 - 3, 2024 | Define the overall application. Write Start Document. |
| Database Design | March 1 - 3, 2024 | Plan the structure of the database to store user data and analytics metrics. |
| User Interface Mockups | March 1 - 3, 2024 | Create and finalize mockups for the analytics dashboard and user settings. |
| Kick-off | March 4, 2024 | Present the idea for the lecturer. |
| Development | March 4 - 8, 2024 | Create a project and build up the starting structures of the project. |
| Development | March 4 - 8, 2024 | Add APIs. Adjust visualization. Add social media registration. |
| Testing | March 9 - 11, 2024 | Test the features and fix the project. |
| Submit | March 12, 2024 | Hand in the project with all the necessary documents and folders. |
| Presentation | March 9 - 11, 2024 | Present the work you have done. |

### 6. User Stories
#### 6.1 Must-Have
| Title | Description |
| --- | --- |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |

#### 6.2 Should-Have
| Title | Description |
| --- | --- |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |

#### 6.3 Could-Have
| Title | Description |
| --- | --- |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |

#### 6.4 Won't-Have
| Title | Description |
| --- | --- |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |
| ? | As a user, I want... |

### 7. Test Plan
#### 7.1 First
| Step | Title | Description | Expected Result |
| --- | --- | --- | --- |
| 7.1.1 | ... | ... | ... |
| 7.1.2 | ... | ... | ... |
| 7.1.3 | ... | ... | ... |

#### 7.2 Second
| Step | Title | Description | Expected Result |
| --- | --- | --- | --- |
| 7.2.1 | ... | ... | ... |
| 7.2.2 | ... | ... | ... |
| 7.2.3 | ... | ... | ... |

#### 7.3 Third
| Step | Title | Description | Expected Result |
| --- | --- | --- | --- |
| 7.3.1 | ... | ... | ... |
| 7.3.2 | ... | ... | ... |
| 7.3.3 | ... | ... | ... |

#### 7.4 Four
| Step | Title | Description | Expected Result |
| --- | --- | --- | --- |
| 7.4.1 | ... | ... | ... |
| 7.4.2 | ... | ... | ... |
| 7.4.3 | ... | ... | ... |

### 8. Additional Information
Virag Szabo | BS | Information Technology | March 2024
