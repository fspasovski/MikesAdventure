# Документација за проектот: Mike’s Adventure

**Студент: Филип Спасовски** <br/>
**Индекс: 151049**

## Опис на играта

Mike’s Adventure претставува едноставна аркадна игра чија основа претставува движење низ мапа и избегнување препреки.
Вие се наоѓате во улога на Mike, чија главна цел е да го одбрани својот свет од злобниот Maiev. Но за да стигне до Maiev, Mike 
мора да собере барем 2500 поени. Mike собира поени на различни начини:
-	Минување низ отворите на препреките (max 100 поени, колку е помала разликата од ширината на отворот и дијаметарот на Mike, толку 
  повеќе поени)
-	Уништување на непријател – војник на Maiev (50 поени)
-	Собирање јаготки (100 поени со P = 0.5)  

### Играта завршува доколку Mike:
-	Се удри во препрека
-	Се удри во непријател
-	Се удри во Maiev или е погоден од неговите удари
-	Го уништи Maiev


## Структура на решението

Оваа игра претставува обид за имплементација на MVC дизајн патернот. 

### Форми:
-	Menu.cs (Воведното мени во играта)
-	Instructions.cs (Инструкции и информации за играта, контроли)
-	View.cs (Активниот прозорец на играта)

### Останати класи:
-	State.cs (Состојба на играта, контролер, ги содржи: Mike, препреките, куршумите, јаготките, непријателите, Maiev, HPbar)
-	Mike.cs (Главниот карактер)
-	Obstacle.cs (Препреки)
-	Enemy.cs (Непријатели)
-	Strawberry.cs (Јаготки)
-	Boss.cs (Maiev)
-	Bullet.cs (Куршуми на Mike и Maiev)
-	HPbar.cs (Сила / енергија на Maiev)

## Опис на класата Obstacle.cs и функцијата State.generateBlocks()

Со помош на класата Obstacle.cs се претставуваат препреките во оваа игра.
```
[Serializable]
public class Obstacle
    {
        public int A { get; set; } // width
        public int B { get; set; } // height
        public bool Worth { get; set; } // is it worth points
        public Point Point {get; set; } // position
        public Color Color { get; set; } // color of the obstacle
        public string Type { get; set; } // type of the obstacle

    }
```
Препреките се всушност правоаголници кои истовремено се придвижуваат надоле со цел да го присилат Mike да ги избегне.  Секоја препрека 
има своја ширина, висина, боја и позиција на која моментално се наоѓа. Минувањето низ препреките е имплементирано на следниов начин:
Покрај наведените особини секоја препрека има тип и property кое означува дали е вредна или пак не. Објектите од класата со 
тип “Obstacle” ги претставуваат вистинските препреки, додека на празните места помеѓу нив се генерираат објекти од класата со
тип “Point” (транспарентни блокови). Со колизија на “Point” правоаголниците и Mike се означува минување на Mike низ препреката и 
добивање поени. Со помош на Worth променливата се овозможува секој премин да носи поени само еднаш.

 

Препреките се генерираат со помош на функцијата State.generateBlocks()
```
public void generateBlocks()
    {

        Random random = new Random();
        int r = random.Next(0, 2);
        if (r == 0)
        {
            int width1 = random.Next(20, WINDOW_WIDTH - 150);
            int gap = random.Next(2 * MIN_SIZE + 10, 2 * MAX_SIZE);
            int width2 = WINDOW_WIDTH - w1 - gap;


            AddObstacle(new Obstacle(new Point(w1 / 2, 30), "Obstacle", Color.SaddleBrown, w1, BLOCK_HEIGHT));
            AddObstacle(new Obstacle(new Point(w1 + gap / 2, 10), "Point" + pointID,  Color.Transparent, gap, BLOCK_HEIGHT / 3));
            AddObstacle(new Obstacle(new Point(w1 + gap + w2 / 2, 30), "Obstacle", Color.SaddleBrown, w2, BLOCK_HEIGHT));
        }
        else
        {
            int gap1 = random.Next(2 * MIN_SIZE + 10, 2 * MAX_SIZE);
            int gap2 = random.Next(2 * MIN_SIZE + 10, 2 * MAX_SIZE);
            int width1 = random.Next(20, (WINDOW_WIDTH - gap1 - gap2) * 60 / 100);
            int width2 = random.Next(20, (WINDOW_WIDTH - w1 - gap2 - gap1) * 80 / 100);
            int width3 = WINDOW_WIDTH - w1 - w2 - gap1 - gap2;

            AddObstacle(new Obstacle(new Point(w1 / 2, 30), "Obstacle", Color.SaddleBrown, w1, BLOCK_HEIGHT));
            AddObstacle(new Obstacle(new Point(w1 + gap1 / 2, 10), "Point" + pointID, Color.Transparent, gap1, BLOCK_HEIGHT / 3));
            AddObstacle(new Obstacle(new Point(w1 + gap1 + w2 / 2, 30), "Obstacle", Color.SaddleBrown, w2, BLOCK_HEIGHT));
            AddObstacle(new Obstacle(new Point(w1 + gap1 + w2 + gap2 / 2, 10), "Point" + pointID, Color.Transparent, gap2, BLOCK_HEIGHT / 3));
            AddObstacle(new Obstacle(new Point(w1 + gap1 + w2 + gap2 + w3 / 2, 30), "Obstacle", Color.SaddleBrown, w3, BLOCK_HEIGHT));
        }
        pointID++;
    }
```

Со P = 0.5 се генерираат две, односно три препреки. Големината на премините е секогаш поголема од минималната големина на Mike. Со конкатенацијата на pointID во типот на објектот се овозможува следново:
Доколку се генерираат три препреки со два премина помеѓу нив, Mike може да добие поени само од еден од нив (низ кој прв ќе помине).

![Alt text](/Screenshots/blockTypes.PNG?raw=true)

## Упатство

Играта започнува со кликање на СТАРТ копчето од почетното мени. Копчето “Инструкции” прикажува прозорец со инструкции и информации за играта. Доколку сакате директно да ја тестирате борбата со Maiev, означете го полето “Тестирај го Maiev” пред да притиснете на копчето “СТАРТ”.

### Контроли
- Mike се движи во четири насоки: горе, доле, лево и десно со помош на стрелките на тастатурата.
- Се зголемува со притискање на Space.
- Се намалува со притискање на Control копчето. 
- Пука со притискање на Z копчето, но секое пукање троши 5 поени.

Играта има вкупно 6 нивоа, секое потешко од претходното.
Моменталната состојба може да се зачува и стартува одново, но исто така може и да се паузира со притискање на ESC копчето.
Ви посакувам пријатна забава во обидот да освоите што повеќе поени!

## Screenshots

![Alt text](/Screenshots/menu.PNG?raw=true)
Слика 1. Главно Мени

![Alt text](/Screenshots/instructions.PNG?raw=true)<br/>
Слика 2. Инструкции

![Alt text](/Screenshots/gameplay.PNG?raw=true)<br/>
Слика 3. Level 1 
 
![Alt text](/Screenshots/bossFight1.PNG?raw=true)<br/>
Слика 4. Борба со Maiev
  
![Alt text](/Screenshots/bossFight2.PNG?raw=true)<br/>
Слика 5. Борба со Maiev

![Alt text](/Screenshots/bossFight3.PNG?raw=true)<br/>
Слика 6. Борба со Maiev
