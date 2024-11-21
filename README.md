## 🔰 Foreword
🇬🇧 A preview is one of the requirements for publishing mods on Nexus, and having a localized preview makes it easier to visually distinguish a translation from other mods without closely examining the page title.

🇺🇦 Прев'ю — є однією з вимог для публікації модифікацій на Nexus, а локалізоване прев'ю дає можливість візуально відрізнити переклад від інших модів, не вглядаючись у назву сторінки.

🇬🇧 However, there are some *"pitfalls"*, such as the `webp` format, which **Nexus Mods** automatically converts all uploaded images into. This format is optimized for storing large images with an alpha channel, but not every editor supports it. If you're lucky, you might find a user-made plugin that adds support for it. Moreover, **Nexus** itself does not allow downloading images in this format directly from the site.

🇺🇦 Однак, існують й *«підводні камені»*, у вигляді формату `webp`, у який конвертуються усі зображення, завантажені на **Nexus Mods**, адже цей формат оптимізований для зберігання важких зображень з альфа-каналом. Проблема полягає в тому, що не кожен редактор підтримує цей формат, дуже пощастить, якщо знайдеться користувацький плагін, який додає його підтримку, крім цього, сам **Nexus** не дозволяє його завантажити на сам сайт.

## The Problem 🥝
🇬🇧 I had a brief "conversation" (if it can be called that) with Dimocracy on Nexus about him removing text from the previews of his translations to save time when preparing them for publication.

This made me ponder the question:
> _"How can we simplify the process of preparing and publishing content on Nexus?"_

🇺🇦 У мене з Dimocracy на Nexus була невелика розмова, стосовно того, що він прибрав написи на прев'ю своїх перекладів, аби зекономити час на оформленні перекладів.

Після цього я задумався над питанням: 
> *«Як можна спростити процес оформлення  та публікації матеріалів на Nexus?»*

🇬🇧 Here's the result of my *"reflections"*: _a small program that automatically overlays pre-made assets onto images._

🇺🇦 І ось результат моїх *«роздумів»*: _невелика програма, яка накладає готові асети на зображення в автоматичному режимі._

# 🔮Magic Compound
🇬🇧 **Magic Compound** lets you combine pre-designed assets with a target image in an automated manner, regardless of the number of assets. Additionally, the program is highly configurable.

🇺🇦 **Magic Compound** дозволяє об'єднювати заготовлені асети з цільовим зображенням без взаємодії з **UI**, незалежно від їх кількості. Також, програма досить гнучка у налаштуванні.

## Update history🧪
**Current version of the program:** **`1.0.2.0`**

## 🧠How Does It Work?
🇬🇧 Drag and drop an image file (png, jpg,  **webp,**  gif) onto the program's executable file, and you'll get the result.  **That's it.**  The program automatically converts and saves the image.

-   **No need to** manually convert files;
-   **No need to** wait for **Photoshop** to open;
-   **No need to** manually position assets on the canvas.  
    _Convenience at its finest._

🇺🇦 Запускаєте саму програму, або перекидаєте у її виконуючий файл зображення (png, jpg, **webp,** gif), і отримуєте результат. **Все.** Програма автоматично конвертує та зберігає зображення. 

 - **Не треба** конвертувати файли самому; 
 - **Не треба** чекати, доки **Photoshop** відкриється;
 - **Не треба** самому розміщувати асети на полотні.
   _Лафа._

🇬🇧 This program is designed to make life easier for modders who repeatedly upload title images and apply the same templates to them day after day.

🇺🇦 Ця програма має на меті спростити життя тим модерам, які день у день завантажують титульні зображення і накладають на них одні й тіж самі шаблони.

## Робота в деталях 🔬
🇺🇦 Щоб розпочати роботу з **Magic Compound** треба заздалегідь підготовити асети, які ви плануєте накладати на зображення, після цього слід перезгенерувати конфігурації, для цього необхідно видалити файл `config.json` у теці **Config** кореневого каталогу програми та запустити її виконуючий файл. 

Після того, як конфінурації будуть успішно згенеровані, слід налаштувати відповідні асети.

## 📚 Asset Configuration
🇬🇧 Configurations allow you to manage the parameters of individual assets *(layers)* and serve as the primary means of interacting with the program. The idea is to set up the configurations to suit your needs during the first use, creating a base template or several templates for ongoing use.

🇺🇦 Конфігурації дозволяють керувати параметрами окремих асетів — *шарів* — та слугують основним засобом взаємодії з програмою. Задум полягає в тому, щоб при першому використанні програми налаштувати конфігурації відповідно до вашої потреби — створивший базовий шаблон, або декілька, для подальшого постійного використання. 

### General Settings ⚙️
🇬🇧 General settings manage the parameters for saving the output image, including its name, format, and storage location.

> ⚠️  **Note:**  Overwriting specific images with updated versions was not intended during development. Therefore, only copies will be created when saving.

-   **OutputName**  — the name of the output image without an extension. You can specify any name, or use the following placeholders:
    -   `%Name%`  — the original image name (default);
    -   `%Index%`  — the image index, useful when saving multiple images.
-   **OutputFormat**  — the output format for:
    -   Static images (`png`,  `jpeg`,  `webp`) as the first argument;
    -   Animated images (`apng`,  `gif`,  `awebp`) as the second argument.

> ⚠️  **Notice:**  To view animations in  `apng`  and  `awebp`, you may need specialized tools, as standard  **Windows**  software might not support them. Alternatively, you can try opening them in a browser window.

> ⚠️  **Notice:**  The  `apng`  and  `awebp`  names are only used for user convenience to distinguish formats. The program internally processes animated images using standard formats like  `png`,  `gif`, and  `webp`.

-   **OutputFolder**  — the folder where the output image will be saved. You can specify any folder or use these path shortcuts:
    -   `%TargetFolder%`  — the folder of the original image;
    -   `%OutputFolder%`  — a subfolder in the program's root directory, created automatically during image saving;
    -   `%ProgramFolder%`  — the folder of the program itself;
-   **AssetsFolder**  — the folder for storing pre-made assets (hidden by default). You can specify any folder or use these path shortcuts:
    -   `%AssetsFolder%`  — a subfolder in the program's root directory;
    -   `%TargetFolder%`  — the folder of the original image;
    -   `%ProgramFolder%`  — the folder of the program itself.
----

🇺🇦 Загальні налаштування керують параметрами збереження вихідного зображення, його ім'ям, форматом, та локацією для збереження. 

>  ⚠️ Зауважте , що під час розробки не було мети перезаписувати конкретні зображення новими оновленими, тому під час збереження, будуть створюватися лише копії. 

- **OutputName** — ім'я вихідного зображення без розширення, ви можете задати будь-яке ім'я, також приймає значення:
  - `%Name%` — ім'я оригінального зображення, за замовчуванням;
  - `%Index%` — індекс зображення, наприклад, якщо ви зберігаєте декілька зображень.
- **OutputFormat** — вихідний формат для:
  -  Статичних зображень (`png`, `jpeg`, `webp`) у першому аргументі;
  -  Анімованих зображень (`apng`, `gif`, `awebp`) у другому аргументі. 

>  ⚠️ Зауважте , що для програвання анімації для `apng` та `awebp` необхідні спеціалізовані засоби, адже стандартні програмний інструментарій **Windows** може не підтримувати їх, але ви можете спробувати переглянути їх, наприклад, перекинувши у вікно вашого браузера.

>  ⚠️Зауважте, що найменування `apng` та `awebp` є лише умовними позначеннями, щоб дати можливість розрізнити їх користувачу. Для того, щоб програма розуміла, як зберігати анімовані зображення, використовуйте стандартні `png`, `gif` та `webp` формати.

- **OutputFolder** — тека збереження вихідного зображення — будь-яка тека у системі, приймає наступні скорочення шляхів:
  - `%TargetFolder%` — тека оригінального зображення; 
  - `%OutputFolder%` — дочірня тека у кореневому каталозі програми, яка створюється автоматично під час збереження зображень;
  - `%ProgramFolder%` — тека самої програми.
- **AssetsFolder** — тека для зберігання готових асетів, за замовчуванням прихована — будь-яка тека у системі, приймає наступні скорочення шляхів:
  - `%AssetsFolder%` — дочірня тека у кореневому каталозі програми; 
  - `%TargetFolder%` — тека оригінального зображення; 
  - `%ProgramFolder%` — тека самої програми.

### 🍂 Layer Configuration
-   **Index**  — the asset layer index, indicating the order in which assets are applied.
-   **Asset**  — the asset name, allowing you to predefine its desired index.

> ⚠️  **Note:**  To define the order before generating settings, prefix the asset's name with the desired index followed by an exclamation mark  (`!`).
> 
> Example:  `1!vignette_hard.png`

-   **Stretch**  — how the asset fills the base image. The following options are available:
    -   `None`  — default; keeps assets at their original size.
    -   `Fill`  — stretches the asset to the full resolution of the target image.
    -   `Uniform`  — stretches the asset while preserving its aspect ratio.
    -   `%Auto%`  — stretches the asset based on the base image proportions, preserving its aspect ratio and cropping it to fit within the base image.
-   **Position**  — the asset’s position relative to the base image and alignment settings:
    -   **X**  — horizontal offset from the starting point.
    -   **Y**  — vertical offset from the starting point.
    -   **IsEmpty**  —  `true`  if both offsets are  **0**, otherwise  `false`.
-   **Opacity**  — the opacity of the asset as a  `float`  value, where  `0.0`  equals  **0%**  and  `1.0`  equals  **100%**.
-   **HorizontalAlignment**  — horizontal alignment determining the origin for the  **Position**  offset on the X-axis:
    -   `Left`  — default; aligns the asset to the left.
    -   `Center`  — centers the asset horizontally.
    -   `Right`  — aligns the asset to the right.
-   **VerticalAlignment**  — vertical alignment determining the origin for the  **Position**  offset on the Y-axis:
    -   `Top`  — default; aligns the asset to the top edge.
    -   `Center`  — centers the asset vertically.
    -   `Bottom`  — aligns the asset to the bottom edge.
- **Index** — індекс шару асетів — відображає порядок, у якому мають накладатися асети.
- **Asset** — ім'я асета, дозволя заздалегіть вказати бажаний індекс для асета.

> ⚠️Зауважте,  що для того, щоб задати програмі порядок перед генеруванням налаштувань, слід вказати на початку імені асета бажаний індекс, і додати знак оклику «`!`» перед початковим ім'ям.
> 
> Наприклад: `1!vignette_hard.png`

 - **Stretch** — заповнення асета — дозволяє визначити, як асет буде заповнювати базове зображення, має наступні параметри:
   - `None` — за замовчуванням, залишає асети в їх оригінальному розмірі;
   - `Fill` — розтягує асет на всю роздільність цільового зображення;
   - `Uniform` — розтягує асет, зберігаючи його співвідношення сторін;
   - `%Auto%` — розтягує асет в залежності від пропорцій базового зображення, зберігаючи його співвідношення сторін та обрізає асет в межах базового зображення.
 - **Position** — позиція асета відносно базового зображення та заданого вирівнювання.
   - **X** — відхилення від початкової точки по горизонталі;
   - **Y** — відхилення від початкової точки по вертикалі;
   - **IsEmpty** — `true`, якщо обидва значення вісей дорівнюють **0**, інакше — `false`.
- **Opacity** — значення непрозорості асета у `float`, де `0.0` дорівнює **0**%, а `1.0` — **100**%.
- **HorizontalAlignment**  — горизонтальне вирівнювання, яке визначає, з якого боку буде вираховуватися відхилення у **Position** по вісі X:
   - `Left` — за замовчуванням, вирівнює асет за лівим боком;
   - `Center` — вирівнює асет за центром по горизонталі;
   - `Right` — вирівнює асет за правим боком.
- **VerticalAlignment** — вертикальне вирівнювання, яке визначає, з якого краю буде вираховуватися відхилення у **Position** по вісі Y:
   - `Top` — за замовчуванням, вирівнює асет за верхнім краєм;
   - `Center` — вирівнює асет за центром по вертикалі;
   - `Bottom` — вирівнює асет за нижнім краєм.

### Робота з декількома пакетами асетів 🌱

Для роботи з декількома пакетами асетів можна створити ярлик програми та у властивостях об'єкта задати параметр:

>  `--Config=шлях-до-файлу-конфігурацій` — без лапок, також підтримуються вищезгадані скороченя.

Далі вам необхідно запустити програму через ярлик, щоб згенерувати нові конфігурації у вказаній теці, а потім додати наступний параметр у новостворених конфігураціях: 
>`"AssetsFolder": "шлях-до-теки-з-асетами",` 

Далі залишається лише налаштувати шари до ваших вподобань.

## 🌊 Watermark 
🇬🇧 The watermark shown is an example of how images can be configured. While I don’t insist on keeping it, it would make me very happy to see that my small program has been useful to its users.

🇺🇦 Водний знак показаний як приклад для налаштування зображень. Я не наполягаю на його збереженні, однак мені б було дуже приємно бачити, що моя невеличка програма стала в нагоді користувачам.
