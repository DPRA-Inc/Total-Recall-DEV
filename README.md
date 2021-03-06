﻿| Node | URL |
|-------|---------|
| Production | https://www.fdachallenge.com |
| Development | https://dev.fdachallenge.com <br />The branch for evaluation is master |
| CI Production | https://www.fdachallenge.com/ccnet |
| CI Development | https://dev.fdachallenge.com/ccnet |

# ShopAware
A prototype application designed to provide users FDA health alerts specific to their shopping list.

##Description (1,421 words)

DPRA followed all the plays of the U.S. Digital Services Playbook in developing the prototype. 

DPRA assigned a Product Manager (Phil Cerasoli to lead the design team. He assigned tasks, ensured the quality of the design, and made all required final decisions. He also established the project budget and schedule.

DPRA assembled a multi-functional team of ten people with significant experience designing and developing secure web and mobile applications using open source products. This team included ten of the Pool 3 labor categories identified in the RFQ. DPRA used two non-technical administrative staff to represent members of the public. Including members of the public on the team ensured that the public’s input was considered and incorporated at every step of the process, so that the product reflected the public’s needs and expectations. 

DPRA created a GitHub repository to serve as a source code version control system, to maintain the product backlog, and to store all work products, documents, files, meeting minutes and artifacts used during development. All members of the team had access to that repository.
Because the RFQ did not specify the application or service to be developed, DPRA’s first step was to generate an idea for a useful service based on the available open source FDA data. To do this, a small group came together to identify a potentially useful service based on the FDA data. The group defined it in a Vision/Concept Description, and included the “human-centered” usability principles to be followed. 

The Agile Coach configured the agile process to be used to support the objectives and time frame of the project; he also adjusted the process during the development (as necessary).

At the kickoff meeting, the Product Manager familiarized the project team with the Vision/Concept of the service. This meeting ensured the team fully understood the idea and the project schedule.

The design team’s objective was to expand on the idea, by generating features and creating a site/UX design that supported the usability principles specified in the Vision/Concept description. The design team’s end state was a package sufficient for the agile development team to begin work.

DPRA assigned a member of the design team to research the needs of the public and why people would use the service, documenting the results in the Vision/Concept statement. The team then met to identify the primary user groups, needs addressed by the service, why users wanted the service, how users currently get the needed information, current method weaknesses, possible usage metrics, initial features, and design themes for the site. The method used by the team to address each objective was to brainstorm ideas without evaluation, and then to discuss and evaluate each idea until the team and the Product Manager reached agreement.  The meeting produced a general design theme, and the Content Designer was assigned an action item to mock up a few web page designs to be evaluated at the next meeting. 

The next meeting was to review the mockups created by the Content Designer and decide on a single site design.  The mockups were discussed and a single design combining concepts from two of the mockups was decided upon by group consensus (with approval by the Product Manager).  For action items, the Content Designer was assigned to produce a storyboard, and the Product Manager was assigned to generate a list of user stories based on the features identified. The storyboard and user stories were sent out to the team for review and comment via email, updates were made based on the feedback received, and the team and Product Manager agreed to all required changes.  

After completion of the basic site design, the team met to brainstorm and select new features to increase the value of the service. Updated storyboards and list of user stories reflecting the results led to a design for final review.

At the final design review the team considered the design ready when the Product Manager and team agreed that it contained sufficient information and detail for development to begin. At this meeting, the Product Manager also selected the features that represented the Minimum Viable Product (MVP).The team created a GitHub Milestone with assigned backlog items, which indicated when the MVP needed to be available.   
  
The Product Manager familiarized the development team with the storyboards and user stories at the next meeting. Here he made sure the developers fully understood the site design and features. After reviewing the storyboards, the team selected a Bootstrap template that most closely supported the proposed design.  The team configured the template to support the purposes of the product, and the DevOps Engineer set up and coordinated hosting the service on the Microsoft Azure cloud platform.
The Product Manager prioritized and groomed the initial product backlog, and worked with the developers to identify the features that could be completed within the timeframe of the project.  The Product Manager also monitored and prioritized the backlog as it changed during development, with input from the rest of the team. 

Led by the Technical Architect (Thomas Kroll), the development team collocated in an office suite to facilitate communication. The Technical Architect maintained frequent communication with the Product Manager and Usability Tester (Theresa DeRosa) to get direction when needed. In addition, the team used Skype as a chat/IM tool.

During development, a short daily standup attended by the Product Manager, Usability Tester and the developers allowed discussion of daily progress, the next day’s agenda, and any issues needing immediate attention. The team used a task board with sticky notes on a white board in the war room during the daily standup to track tasks. The Product Manager used a release burn up chart to track progress.

Development and usability testing was iterative and constant.

Upon each developer check in to a dev-branch, the code was automatically built and deployed to the dev-branch node.  Once a branch was merged to the master, the development and production nodes were updated automatically with a new build through our continuous integration server.  The continuous integration server also handled the automated unit tests using NUnit.  The Usability Tester and Product Manager received automated email alerts once the main production branch was updated. This email contained the results of the unit tests and build summaries facilitating the testing and review process. 

After each daily production build, the Usability Tester reviewed and tested the changes from that day, and the entire team conducted a short review of the build, including at least one representative of the public. The team added issues raised during testing and review to the backlog as “Issues” and labeled them as “Enhancements” or “Bugs” as appropriate. The Agile Coach conducted a retrospective as part of the daily review, serving as an “inspect and adapt” point in which the process was evaluated and adjustments made.

A focus group representing the public evaluated and provided feedback on the service. The Usability Tester facilitated two reviews of the product by the focus group.  The team reviewed and prioritized the feedback and entered it into the product backlog.  The most useful and practical suggestions that could be completed in the time allotted were implemented.

After code peer review, the team corrected all issues found. The team scanned code for possible security vulnerabilities, and addressed issues.
The team maximized use of open source and other free products with more than twenty open source tools in use.

The team followed DPRA’s Information Security Continuous Monitoring (ISCM) procedures. The purpose of DPRA’s ISCM program is to (1) provide assurances that the network environment maintains a given security posture, (2) provide a consistent environment for the user to interact with systems, (3) ensure safeguards designed into the architecture remain in effect over time and in multiple locations, (4) to comply with regulatory guidance to monitor activities/changes on the network infrastructure.

The team performed a full system test of the final release candidate and resolved critical issues. The product finalized prior to the second change in submission date had more features then initially planned. As the due date was extended, the agile approach allowed maximum flexibility to add new capability without destabilizing the product. In addition, the designers continued to work to advance the design during development, in case there was time to add additional features. These designs included our plans for a mobile application on multiple devices. 

Finally, the extra time allowed by the extension was used to construct an additional version of the site using ASP.NET 5 Core, the open source Microsoft solution which allows for distribution to any OS: OS/X, Linux, and Windows.  **END OF DESCRIPTION**

##Criteria and Evidence

> A.  Assigned one leader, gave that person authority and responsibility, and held that person accountable for the quality of the prototype submitted

See Team Roster Document in Total-Recall-DEV / Documentation <br /><br />Phil Cerasoli has over 28 years of experience developing software models and systems as a technical team management.  As a principal and Chief Technology Officer for DPRA, Phil is responsible for the development of all information technology applications and the management of over 30 technical personnel. This includes responsibility for long-range planning, technical analysis and design, policy and procedure development, programming standards, configuration management, quality assurance, technical documentation, staffing, supervision of team leaders, maintaining knowledge of emerging technologies, and serving as lead engineer on selected projects.

---

> B.  Assembled a multidisciplinary and collaborative team including a minimum of 5 labor categories from the Development Pool labor categories to design and develop the prototype

See Team Roster Document in Total-Recall-DEV / Documentation<br />See the “Issues” page in GitHub<br /><br />DPRA used 10 of the 13 full stack labor categories for this effort. The agile process that we used included documenting the Meeting Minutes and daily standups. We used GitHub collaboratively by using Issues and inserting comments on the Issues. See the “Issues” page in GitHub for a record of all issues, and their related conversations.

---

> C.  Understand what people need, by including people in the prototype development and design process 

See Total-Recall-DEV / Usability Testing <br /><br />We used two non-technical resources throughout the project to act as the public. These resources brought different perspectives based on demographics. The team used these resources at multiple points throughout the project to provide input as representatives of the public. The development team also used a larger focus group facilitated by the usability tester to gain further insights from the public. Ordinarily we would use anticipated users of the system, and the actual public. However, for ease of access, and due to the limited time frame, DPRA used existing employees who were not on the design or development team. 

---

> D. Used at least three "human-centered design" techniques or tools 

See Document “Human Centered Design” in Total-Recall-DEV / Documentation<br /><br />The techniques and tools used are as follows: <br /><br />1. Identified the people that would use the website and what they would use it for. We targeted primarily a 30+ demographic, parents and elderly that are concerned with their health and the health of the family. The Shopping List “item check” idea was conceived to attract the parent that would be the primary shopper and purchase items for the family and elderly that might have a larger concern with medication and medical devices. <br /><br />2. Identified the goals of the website. The goal was to bring the FDA supplied recall and adverse event data to the user instead of the user having to search for the information they were interested in. Allowing the users to enter items quickly and know immediately if the products they use have any recalls or events associated with them. The users’ general location would also be collected so the information would be specific to their area of the country.   <br /><br />3. Design solutions to meet the website goals and appeal to the website demographic. <br /><br />DPRA used 15 techniques that supported human centered design. See the referenced document for details.

---

> E. Created or used a design style guide and/or a pattern library 

See Total-Recall-DEV / SourceCode<br /><br />We used Bootstrap to enforce the design style. <br /><br />Bootstrap is an open source design framework. DPRA selected a template that had been built that was closest to our design team’s vision. This framework is a part of the source code. 

---

> F. Performed usability tests with people 

See Total-Recall-DEV / Usability Testing<br /><br />The usability tester coordinated tests with the focus group twice throughout development cycle. The folder referenced contains the survey questions, results, feedback, and other relevant documents. 

---

> G. Used an iterative approach, where feedback informed subsequent work or versions of the prototype 

See Total-Recall-DEV / Meeting Minutes <br />See the “Issues” page in GitHub<br />See the 1500 word description for in depth process<br /><br />We relied on feedback from developers, designers, testers, and “the public” throughout this process in order to develop our work list. The product was reviewed daily in order to solicit feedback from the team and “the public.” 

---

> H. Created a prototype that works on multiple devices, and presents a responsive design 

See https://www.fdachallenge.com<br /><br />The prototype works on multiple browsers, and is configured to be easy to use on mobile devices of various sizes. It was tested on multiple browsers (Chrome, IE, FireFox, etc) and devices (Apple IPADs, Iphones, Android phones, PCs, etc). 

---

> I. Used at least five modern and open-source technologies, regardless of architectural layer (frontend, backend, etc.) 

We used the following tools and information sources in the design and development of our prototype. 

CSS - Cascading Style Sheet

HTML - HyperText Markup Language 

DOM - Document Object Model 

JS – JavaScript

CI – Continuous Integration

JSON – JavaScript Object Notation

FDA – United States Food and Drug Administration


* Freenetlaw (provided the privacy statement) 

* http://www.fda.gov/AboutFDA/ContactFDA/StayInformed/RSSFeeds/Recalls/rss.xml (provided the news feed on the front page) 

* http://sass-lang.com/<br />CSS preprocessor which allows more advanced directives and capabilities

* http://lesscss.org/<br />CSS preprocessor which allows more advanced directives and capabilities

* https://angularjs.org/<br />JS library which enables dynamic manipulation of the HTML DOM 

* http://openlayers.org/<br />JS library for adding maps to websites 

* http://getbootstrap.com/<br />A predefined, highly customizable, CSS for use in styling websites

* http://cruisecontrolnet.org/ <br />Automated CI build server

* https://github.com/tombatossals/angular-openlayers-directive<br />JS library which exposes the Open Layers library to AngularJS

* http://www.chartjs.org/<br />JS library for adding charts and graphs to a website

* https://sroze.github.io/ngInfiniteScroll/<br />JS library to enable “infinite scrolling,” which is dynamically appending content to a web page or control as needed

* http://jqueryui.com/ <br />JS library with predefined user controls and interactions

* https://github.com/onokumus/metisMenu<br />JS library enabling a customizable menu system

* https://oclazyload.readme.io/<br />AngularJS module which enables loading other modules only when needed

* http://github.hubspot.com/pace/<br />JS library and CSS definition for a progress bar

* https://github.com/benpickles/peity<br />JS library for adding charts and graphs to a website

* https://github.com/rochal/jQuery-slimScroll/<br />JS library which transforms “div” controls into “scroll-able” user controls

* https://github.com/CodeSeven/toastr<br />JS library to enable on-screen notifications to the user

* https://github.com/johan/world.geo.json<br />Annotated geo-files in JSON format for use in map controls

* https://github.com/cornflourblue/angulike<br />AngularJS directives for social sharing buttons - Facebook Like, Google+, Twitter and Pinterest 

* https://spoon.net/<br />Application virtualization container which allow the application to run on any Windows machine without additional configuration or software installation

* https://open.fda.gov/<br />Public FDA data sharing portal 

---

> J. Deployed the prototype on an Infrastructure as a Service (iaas) or Platform as a Service (paas) provider, and indicated which provider they used 

We used Microsoft Azure cloud environment, which acts as an IaaS. <br /> <br /> “IaaS is a managed compute service that gives complete control of the OS and the application platform stack to the application developers and IT Professional which is analogous to Hyper-V and other virtualization platforms. The unit of deployment is at the granularity of a virtual machine. Developers with the help of IT Professionals deploy virtual machines, application bits and the associated data to the target compute infrastructure. Even though developers get complete control of the stack at the design time, the deployment still needs to consider the systemic qualities of the application influenced by the storage, virtual networking and the managed services ecosystem that surrounds it. While IaaS gives design time portability, in due course the application may take advantage of the managed services (e.g. Azure Storage, cache) that will impact its overall portability. Similar to the analysis of PaaS, we will take look at the advantages and disadvantages of IaaS from both business and technology angles.” <br /><br />http://blogs.msdn.com/b/hanuk/archive/2013/12/03/which-windows-azure-cloud-architecture-paas-or-iaas.aspx 

---

> K. Wrote unit tests for their code 

See Total-Recall-DEV / SourceCode / Tests / ApiDalc.Tests<br /><br />Automated unit tests (NUnit) were developed that ran automatically within the integrated development environment (IDE), and results were appended as part of the automated build process. 

---

> L. Set up or used a continuous integration system to automate the running of tests and continuously deployed their code to their iaas or paas provider 

See Total-Recall-DEV / Documentation / Build and Continuous Integration Server<br />See Total-Recall-DEV / Build<br /><br />CruiseControl was used for continuous integration. In addition an email alert was sent to the product manager and usability tester after each automated deployment. The document referenced is our instructions for replicating a CruiseControl installation. The “Build” folder referenced is the node configuration for CruiseControl along with all tools necessary to build, email, unit test, and deploy. 

---

> M. Set up or used configuration management 

See the “Issues” page in GitHub<br /><br />We used GitHub “Issues” to track and approve work. The product manager had final approval of all proposed software changes. DPRA has an existing CM process that it has developed in conjunction with its Federal work. We used this CM process for the development of this application. 

---

> N. Set up or used continuous monitoring 

See Continuous Monitoring document in Total-Recall-DEV / Documentation<br /><br />The document referenced goes into detail on our cyber and physical security processes.  

---

> O. Deploy their software in a container (i.e., utilized operating-system-level virtualization) 

See Run Prototype Locally document in Total-Recall-DEV / Documentation<br /><br />We used Spoon (the open source tool) as the container for easy deployment. The document referenced describes how to run the application in a virtual container on a PC. 

---

> P. Provided sufficient documentation to install and run their prototype on another machine 

See Run Prototype Locally document in Total-Recall-DEV / Documentation<br /><br />See Install and Debug Prototype document in Total-Recall-DEV / Documentation<br /><br />In addition to the virtual deployment document, we have included a document on building, running, and publishing the prototype (Insall and Debug Prototype document). 

---

> Q. Prototype and underlying platforms used to create and run the prototype are openly licensed and free of charge 

The prototype and its underlying platforms are openly licensed and free of charge. 
