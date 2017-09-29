# eNeg

eNeg is a system that has two main targets:

*	Collect as much information as possible from different users‘ communication channels via Add-ons and store it centralized in one place.
* Use this collected information as input for data analysis through Apps and allow these Apps to provide valuable support information to users.

The FrameWork contains different sub-systems to achieve these targets:

* Desktop Add-on that can collect information.
* Web Platform that shows all collected information centralized at one place.
* Apps that analyse the collected information in terms of preferences, strategy, cultural aspects and more.

eNeg is a silverlight web based platform with desktop add-on extension, MVVM framework applied with n-tier layers.

* RIA Services
* Entity Framework
* Logging
* Excption Handling
* MEF

For more details please find the [Software Requirements Specification](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/SRS_eNeg_Negotiation_Framework.docx), the [Technical Design Specification](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/eNeg_TDS_KR.docx), the [Architecture](https://github.com/ivconsult/eNeg/blob/master/eNeg%20Documentation/eNEG%20Infrastructure%20logical%20Architecture.docx) and some videos explaining the uses cases of eNeg in [eNeg Documentation](https://github.com/ivconsult/eNeg/tree/master/eNeg%20Documentation).

## Features

* Allow users to track and collect information messages from different communication channels
* Create a unlimited number of online negotiations.
* Multi communication channel support
* Negotiate with different counterparts via different channels managed at one place
* Track the whole history of a negotiation
* View and manage negotiations (ongoing, past)
* See all details of a negotiation event
* Addionally 7 different apps for negotiation support integrated: [PrefApp](https://github.com/ivconsult/eNeg-PrefApp), [Issue App](https://github.com/ivconsult/eNeg-IssueApp), [CultureApp](https://github.com/ivconsult/eNeg-CultureApp), [StrategyApp](https://github.com/ivconsult/eNeg-StrategyApp), [MessageApp](https://github.com/ivconsult/eNeg-MessageApp), [eSourceApp](https://github.com/ivconsult/eNeg-eSourceApp), [OfferApp](https://github.com/ivconsult/eNeg-OfferApp)

## Setting Development Environment

* A .NET Integrated Development Environment (IDE) such as Visual Studio or the free Visual Web Developer Express
* Install Microsoft Silverlight runtime for [windows](https://go.microsoft.com/fwlink/?LinkId=229324). (This is the runtime that’s required for Silverlight applications).
* Install [Silverlight Toolkit](https://silverlight.codeplex.com/releases/view/78435)
* Install [Silverlight SDK](https://www.microsoft.com/en-us/download/details.aspx?id=28359)
* Install [Silverlight Tools for VS 2010](https://www.microsoft.com/en-us/download/details.aspx?id=28358) (Optional)
* Install [Expression Blend](https://www.microsoft.com/en-eg/download/details.aspx?id=3062). This is a design tool that allows users to interact with Silverlight.

## References
### Following open-source projects were used:
* [Clog Client Logging](http://clog.codeplex.com) under [LGPL](http://clog.codeplex.com/license)
* [MVVM Light Toolkit](http://www.mvvmlight.net) under [MIT License](http://mvvmlight.codeplex.com/license)
* [iTextSharp](https://github.com/itext/itextsharp) under [AGPL](https://github.com/itext/itextsharp/blob/develop/LICENSE.md)
* [Rhino Mocks](https://github.com/ayende/rhino-mocks) under [BSD 3-clause "New" or "Revised" License](https://github.com/ayende/rhino-mocks/blob/master/license.txt)
* [silverPDF](https://silverpdf.codeplex.com/) under [MIT License](https://silverpdf.codeplex.com/license)

### Also used were proprietary control from 
* [Telerik](http://www.telerik.com/products/wpf/overview.aspx) (**not included in sources !**)
