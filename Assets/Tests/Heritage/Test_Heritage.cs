using NUnit.Framework;
using TU_Challenge.Heritage;

/// <summary>
/// on attaque l'héritage avec virtual/override, évènements et protected
/// Pour rendre les tests visible, tu dois passer le "#if false" à "#if true" ligne 14
/// </summary>
public class Test3_Heritage
{
    [Test]
    public void SimpleTest()
    {
        Assert.Pass();
    }


    [Test]
    public void ChatEstUnAnimalEtPossedeUnNom()
    {
        Chat cat = new Chat("Matou");

        Assert.IsTrue(cat is Animal);
        Assert.That(cat.Name, Is.EqualTo("Matou"));
    }


    [Test]
    public void ChatFaitMiaou()
    {
        Chat cat = new Chat("Garfield");

        Assert.That(cat.Crier(), Is.EqualTo("Miaou (j'ai faim)"));
    }

    bool _flag;
    public void DetectDie()
    {
        _flag = true;
    }

    [Test]
    public void ChatPeutMourrir() // Pour rappel aucun animal n'a été maltraité durant le test unitaire
    {
        Chat cat = new Chat("Chapin");

        // Arrange
        _flag = false;
        cat.OnDie += DetectDie;

        // Act
        cat.Die();

        // Assert
        Assert.IsTrue(_flag);
        cat.OnDie -= DetectDie;
    }

    [Test]
    public void ChatPattes()
    {
        Chat cat = new Chat("Nougat");

        Assert.That(cat.Pattes, Is.EqualTo(4));

    }

    [Test]
    public void ChatBoiteux()
    {
        ChatQuiBoite catBoiteux = new ChatQuiBoite("Boite");
        Chat cat = new Chat("Pas Boite");

        Assert.IsTrue(catBoiteux is Chat);
        Assert.IsTrue(catBoiteux is Animal);
        Assert.That(catBoiteux.Pattes, Is.EqualTo(3));

        Assert.IsTrue(cat is Chat);
        Assert.IsTrue(cat is Animal);
        Assert.That(cat.Pattes, Is.EqualTo(4));
    }

    [Test]
    public void CreerAnimalerie()
    {
        Animalerie animmalerie = new Animalerie();
        Chat cat = new Chat("Ganache");

        animmalerie.AddAnimal(cat);

        Assert.IsTrue(animmalerie.Contains(cat));
    }

    [Test]
    public void CreerAnimalerieAvecChatEtChien()
    {
        Animalerie animalerie = new Animalerie();
        Chat chat = new Chat("Praline");
        Chien chien = new Chien("Rufus");

        animalerie.AddAnimal(chat);
        animalerie.AddAnimal(chien);

        Assert.IsTrue(animalerie.Contains(chat));
        Assert.IsTrue(animalerie.Contains(chien));
    }

    [Test]
    public void CreerAnimalerieEtLesFaireCrier()
    {
        Animalerie animalerie = new Animalerie();
        Chat chat = new Chat("Praline");
        Chien chien = new Chien("Rufus");

        animalerie.AddAnimal(chat);
        animalerie.AddAnimal(chien);

        Assert.That(animalerie.GetAnimal(0).Crier(), Is.EqualTo("Miaou (j'ai faim)"));
        Assert.That(animalerie.GetAnimal(1).Crier(), Is.EqualTo("Ouaf (j'ai faim)"));
    }

    [Test]
    public void CreerAnimalerieEtLesFaireCrierPuisNourrir()
    {
        Animalerie animalerie = new Animalerie();
        Chat chat = new Chat("Praline");
        Chien chien = new Chien("Rufus");

        animalerie.AddAnimal(chat);
        animalerie.AddAnimal(chien);

        Assert.That(animalerie.GetAnimal(0).Crier(), Is.EqualTo("Miaou (j'ai faim)"));
        Assert.That(animalerie.GetAnimal(1).Crier(), Is.EqualTo("Ouaf (j'ai faim)"));
        animalerie.FeedAll();

        Assert.That(animalerie.GetAnimal(0).Crier(), Is.EqualTo("Miaou (c'est bon laisse moi tranquille humain)"));
        Assert.That(animalerie.GetAnimal(1).Crier(), Is.EqualTo("Ouaf (viens on joue ?)"));
    }


    bool _zooEvent;
    void DetectAddAnimal(Animal addedAnimal)
    {
        _zooEvent = true;
    }

    [Test]
    public void CreerZooEtRecevoirEvent()
    {
        Animalerie animalerie = new Animalerie();
        Chat chat = new Chat("Reglisse");

        _zooEvent = false;

        animalerie.OnAddAnimal += DetectAddAnimal;
        animalerie.AddAnimal(chat);

        Assert.IsTrue(_zooEvent);
    }

    [Test]
    public void LesAnimauxPeuventInteragirDansLanimalerie()
    {
        Animalerie animalerie = new Animalerie();
        Poisson poisson = new Poisson("Bubulle");
        Chat chat = new Chat("Radis");

        Assert.That(chat.Crier(), Is.EqualTo("Miaou (j'ai faim)"));
        animalerie.AddAnimal(poisson);

        Assert.That(poisson.IsAlive, Is.EqualTo(true));

        animalerie.AddAnimal(chat);

        Assert.That(poisson.IsAlive, Is.EqualTo(false));  // Oups
        Assert.That(chat.Crier(), Is.EqualTo("Miaou (Le poisson etait bon)"));
    }

    [Test]
    public void LesAnimauxPeuventSinscrireACetEvent()
    {
        Animalerie animalerie = new Animalerie();
        Poisson poisson = new Poisson("Bubulle");
        Chat chat = new Chat("Radis");

        Assert.That(poisson.IsAlive, Is.EqualTo(true));
        Assert.That(chat.Crier(), Is.EqualTo("Miaou (j'ai faim)"));

        animalerie.AddAnimal(chat);

        animalerie.AddAnimal(poisson);

        Assert.That(poisson.IsAlive, Is.EqualTo(false));  // Oups
        Assert.That(chat.Crier(), Is.EqualTo("Miaou (Le poisson etait bon)"));
    }

    [Test]
    public void MaisChatBoiteuxNeMangePasLePoisson()
    {
        Animalerie animalerie = new Animalerie();
        Poisson poisson = new Poisson("Bubulle");
        ChatQuiBoite chat = new ChatQuiBoite("PasDePat(trouille)");

        Assert.That(chat.Crier(), Is.EqualTo("Miaou (j'ai faim)"));
        animalerie.AddAnimal(poisson);

        Assert.That(poisson.IsAlive, Is.EqualTo(true));

        animalerie.AddAnimal(chat);

        Assert.That(poisson.IsAlive, Is.EqualTo(true));
        Assert.That(chat.Crier(), Is.EqualTo("Miaou (j'ai faim)"));
    }

#if false
    [Test]
    public void Poisson()
    {
        Poisson p = new Poisson("Nemo");

        Assert.That(p.Pattes, Is.EqualTo(0));
    }

    [Test]
    public void PoissonRajouteUnSuffixASonNom()
    {
        Poisson p = new Poisson("Nemo");

        Assert.That(p.Name, Is.EqualTo("Nemo le poisson"));
    }
#endif
}
