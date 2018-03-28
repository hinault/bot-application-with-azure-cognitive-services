# bot-application-with-azure-cognitive-services

## English version

This repository is a bundle of projects on Bot Framework and Azure Cognitives Services. You can use it to discovert how to build smart conversationnal agent with Bot Framework and Azure Cognitive Services.

## FormFlow Basic

FormFlow is a powerful tool for the creating with minus complexity a conversational agent with Bot Builder SDK for .NET.FormFlow automatically generates the dialogs that are necessary to manage a guided conversation, based upon guidelines that you specify. Designing a guided conversation using FormFlow can significantly reduce the time it takes to develop your bot.

This is a basic code sample which show you how you can use FormFlow for build a survey bot.

For more information read this tutorial https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow

## FormFlow Advanced

FormFlow Advanced show how you can use the advanced features of FormFlow to deliver a more customized user experience.

For more information read this tutorial https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow-advanced

## QnA Maker

Microsoft QnA Maker is a free, easy-to-use, REST API and web-based service that trains AI to respond to user's questions in a more natural, conversational way. With optimized machine learning logic and the ability to integrate industry-leading language processing with ease, QnA Maker distills masses of information into distinct, helpful answers.

The QnA Maker project is a sample of how you can build smart bot which can give answer at a question from user.

For more details about QnA Maker read this tutorial
https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/home 

## Text Analytics API

Text Analytics API is a cloud-based service that provides advanced natural language processing over raw text, and includes three main functions: sentiment analysis, key phrase extraction, and language detection.

The code sample show how you can process  analysis on user input and determine how he feels.

For more details about Text Analytics AP read this tutorial https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/quickstarts/csharp

## LUIS

Language Understanding (LUIS) allows your application to understand what a person wants in their own words. LUIS uses machine learning to allow developers to build applications that can receive user input in natural language and extract meaning from it. A client application that converses with the user can pass user input to a LUIS app and receive relevant, detailed information back.

LuisBot is a basic sample of how you can integrate LUIS with your bot.

For more details about LUIS read this tutorialhttps://docs.microsoft.com/fr-ca/azure/cognitive-services/LUIS/Home

## PoutineBot

PoutineBot is a smart bot which use LUIS, FormFow, Text Analytics API and QnA Maker.  PoutineBot can converse with user using the natural language. It can process an order or provide  an answer at user’s question. PoutineBot also make sentiment analysis when it receive feedback from user, for determine if the feedback is negative or positive.

## Version Française

Ce repository contient un ensemble de projets sur Bot Framework et Azure Cognitive
Services. Ces derniers permettent de découvrir comment mettre en place un agent
conversationnel intelligent en utilisant Bot Framework et Azure Cognitive
Services.

 
## FormFlow Basic

FormFlow est un puissant outil pour mettre en place avec le minimum de complexité un
agent conversationnel capable de guider l’utilisateur dans un dialogue (processus
de commande, sondage, etc.).

FormFlow Basic est un exemple de code simple de comment utiliser FormFlow dans une botapplication. 

Pour en savoir plus, consultez le tutoriel suivant : https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow

## FormFlow Advanced

FormFlow Advanced montre comment utiliser les options avancées de FormFlow pour
personnaliser le bot et offrir une meilleure expérience utilisateur.

Pour en savoir plus, consultez le tutoriel suivant :https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-formflow-advanced

## QnA Maker

QnA Maker est une API REST et un service web permettant de créer et entrainer une
intelligence artificielle qui sera en mesure de répondre aux questions d’un
utilisateur à travers une conversation en langage naturel.

Le projet QnA Maker est un exemple de comment rendre son bot plus intelligent en intégrant
QnA Maker. Le Bot mis en place utilise QnA Maker afin de fournir une réponse
aux questions des utilisateurs.

Pour en savoir plus sur QnA Maker, consultez la documentation suivante : https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/home 

## Text Analytics API

Text Analytics API est un service Cloud qui offre des fonctionnalités avancées d’analyse
de texte. Elle dispose de trois fonctionnalités majeures : l’analyse de
sentiment, l’extraction des mots-clés et la détection de la langue pour un
texte.  L’analyse de sentiment peut, par
exemple, être utilisée pour évaluer le degré de satisfaction d’un utilisateur
en procédant en une analyse de ses écrits.

Le projet Text Analytics montre comment faire appel au service dans un bot.

Pour en savoir plus sur le service, consultez la documentation suivante : https://docs.microsoft.com/en-us/azure/cognitive-services/text-analytics/quickstarts/csharp

## LUIS 

Pour demander un service (passer par exemple une commande), plusieurs expressions
peuvent être utilisées. Comment déterminer le besoin de l’utilisateur au
travers de son texte ? C’est à ce besoin que LUIS (Language Understanding) répond.
Luis permet à votre application de comprendre ce que l’utilisateur veut en ses
propres mots. LUIS utilise l’apprentissage machine pour permettre aux développeurs
de créer des applications en mesure de comprendre le langage naturel, ainsi que
les besoins d’un utilisateur.

LuisBot est un exemple simple d’intégration d’un bot avec LUIS.

Pour en savoir plus, consultez la documentation suivante : https://docs.microsoft.com/fr-ca/azure/cognitive-services/LUIS/Home

## Poutine Bot

PoutineBot est un bot intelligent qui exploite Luis, FormFlow, QnA Maker et Text Analytics
API pour interagir intelligemment avec un utilisateur. Ce dernier permet
notamment de passer des commandes de poutine (pour information, la poutine est
un plat québécois), de répondre à des questions et d’analyser les feedbacks des
utilisateurs pour évaluer leur degré de satisfaction.

Le projet PoutineBot est un excellent moyen de découvrir comment exploiter
FormFlow, Luis, QnA Maker et Text Analytics API dans un bot et gérer le flow de
communication entre ces différents services. 

