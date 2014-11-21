----------------------------------------------------------------------------
SITECORE ORM O/R CODE GENERATOR
----------------------------------------------------------------------------
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

----------------------------------------------------------------------------
WHAT IS IT?
----------------------------------------------------------------------------
    The program will create a class for each of your Sitecore templates,
    hence "o/r mapping" a Sitecore template to a C# class.

    It is a framework; you can change what code to generate simply by
    changing some .aspx pages.

----------------------------------------------------------------------------
INSTALLATION
----------------------------------------------------------------------------
    Unzip the files to the Sitecore installation you wish to generate code
    for.
    There is no Sitecore package, no Sitecore items.

----------------------------------------------------------------------------
HOW TO USE IT
----------------------------------------------------------------------------
    Execute the following page:
      http://yourwebsite/sitecore%20modules/Shell/SitecoreORM/Execute.aspx                
    
    By default, the generated C# code will be placed in:
      /sitecoreorm/

----------------------------------------------------------------------------
CHANGING HOW THE C# CLASS ARE RENDERED
----------------------------------------------------------------------------
    Each C# code file is rendered from an .aspx page:
    
      \sitecore modules\Shell\SitecoreORM\CodeTemplate.aspx

    This page is called with the template ID as parameter.
    It uses standard properties to output what it wants to output, like:
    
      <%# Template.ID %>

    The page has the following properties:

      // The template that is beging O/R mapped
      TemplateItem Template;

      // The namespace of the C# class
      string Namespace

      // The class name of the C# class
      string ClassName

      // A method that allows you to plug in any code from any assembly
      // A codeprovider must inherit from the ICodeProvider interface
      // and implement the Get(TemplateItem template, params string[] parameters);
      // method. The output of a codegenerator is a text string.
      // Code generators can generate anything you would like to use 
      // in your class.
      string ExecuteCodeProvider(string assemblyAndClass, params string[] parameters)

    To change how the generated, you should modify the contents of the 
    \sitecore modules\Shell\SitecoreORM\CodeTemplate.aspx file.

----------------------------------------------------------------------------
CHANGING HOW THE FIELDS ARE RENDERED
----------------------------------------------------------------------------
    By default fields are rendered using .aspx pages located at:
    
      \sitecore modules\Shell\SitecoreORM\FieldTemplates\*

    There are a seperate field template for each field type in Sitecore.
    Please notice that Sitecore combines the field types for several
    fields, f.ex. most lookup fields have the same field type.

    If the field type cannot be determined, the "UnknownFieldType.aspx"
    is executed.

    You can modify how each field are rendered by changing each file.

----------------------------------------------------------------------------
CHANGING HOW INHERITED TEMPLATES ARE RENDERED
----------------------------------------------------------------------------
    By default inherited templates are rendered using an .aspx page
    located at:

      \sitecore modules\Shell\SitecoreORM\InheritedTemplateTemplates\InheritedTemplate.aspx

    By default inherited templates are rendered as a property that can
    be accessed on the inherited template.

----------------------------------------------------------------------------
PLUGGING IN YOUR OWN CODE GENERATOR
----------------------------------------------------------------------------
    You can plug in code at the class code file:
    
      \sitecore modules\Shell\SitecoreORM\CodeTemplate.aspx

    Use the ExecuteCodeProvider(string, params[]) metod to generate code.

    ----------------------------------
    STEP1: CREATE A NEW CODE PROVIDER:
    ----------------------------------
     
    Generate a class in your project that inherits from the ICodeProvider
    interface.

    For example:
    
      public class GenerateCode : ICodeProvider
      {
        public string Get(TemplateItem template, params string[] parameters)
        {
          string parameter0 = parameters[0];
          return "Simon says: " + parameter0;
        }
      }

    ----------------------------------
    STEP2: PLUG IN THE CODE GENERATOR:
    ----------------------------------
     
    Add the following line to the CodeTemplate.aspx:

      <%# ExecuteCodeProvider("MyClass, MyDll", "code generated") %>

    The following code will be added to your C# class:

      Simon says: code generated

----------------------------------------------------------------------------
CONFIGURATION
----------------------------------------------------------------------------
    The code generator uses the following configuration file:

      \App_Config\Include\sitecoreorm.config

    --------------------------
    HOW TO MODIFY CLASS NAMES:
    --------------------------

    Class names are created by combining the template name (or a C# code
    safe verion of the template name) + a suffix from the configuration
    called:

      ClassNameSuffix (Default: "Item")

    By default, a template called MyTemplate will be called MyTemplateItem
    Change this behavior by changing the ClassNameSuffix.

    --------------------------
    HOW TO MODIFY NAME SPACES:
    --------------------------

    Name spaces are determined from the template path in Sitecore, but can 
    be manipulated by 2 properties:

    NameSpacePrefix (Default: "Sitecore.Data.Items.")
    NameSpaceRemove (Default: "/")

    Imagine a template located at /templates/Project/MyTemplate.

    NameSpacePrefix adds a namespace in front of the path. The default
    namespace will be:

      Sitecore.Data.Items.Project.

    And the class will be:

      Sitecore.Data.Items.Project.MyTemplateItem

    NameSpaceRemove removes path sections from the namespace.
    By default, nothing is removed.
    To remove "Project" from the namespace, add the following to the
    property:

      Project.

    ---------------------------------
    HOW TO EXCLUDE (IGNORE) TEMPLATES
    ---------------------------------

    Templates to be ingored are located at:

      IgnoreTemplateRoots (Default: "system/", "common/")

    You write the template folder path that you wish not to generate code for.
    Any templates below the specified template roots will be ignored.

    ------------------------------
    HOW TO INCLUDE (ADD) TEMPLATES 
    ------------------------------   

    Templates to add are located at:

      IncludeTemplateRoots (Default: "/")

    You write the template folder path that you wish to generate code for.
    If the template folder added are also on the IgnoreTemplateRoots list,
    the template will be ignored.

    By default, the root is added, but system/ and common/ ignored,
    hence generating code for anything but common/ and system/ templates.

    To add a specific folder, add a template root:

      <TemplateRoot>folder/</TemplateRoot>
      <TemplateRoot>folder/folder/</TemplateRoot>

    -------------------------------------------------------------
    HOW TO DETERMINE WHERE C# CLASSES ARE WRITTEN (FILE LOCATION)
    -------------------------------------------------------------

    File location can be manipulated 2 places:

    The default file location has the following setting:

      DefaultFileDestination (Default: "/sitecoreorm/")

    Files are placed according to their namespace. The code
    generated for the following template:

      /templates/Project/MyTemplate

    Will be located at:

      /sitecoreroot/sitecoreorm/Sitecore/Data/Items/Project/MyTemplate.cs

    If you have specified several template roots at IncludeTemplateRoots 
    you can determine an output folder for each of the roots folders.
    This is nifty when working with component development, where each 
    component is located in it's own folder.
    To add a specific destination folder for a specific template root, and
    the "fileDestination" attribute:

      <IncludeTemplateRoots hint="raw:AddIncludeTemplateRoots">
        <TemplateRoot fileDestination="d:\project\code\components\document">/components/document/</TemplateRoot>
        <TemplateRoot fileDestination="d:\project\code\components\navigation">/components/navigation/</TemplateRoot>
        <TemplateRoot fileDestination="d:\project\code\components\identity">/components/identity/</TemplateRoot>                 
      </IncludeTemplateRoots>
