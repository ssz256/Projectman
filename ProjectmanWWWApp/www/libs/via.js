const VIA_PREFIX = "vi-"
const VIA_APP_ATRIB = VIA_PREFIX + "app";  
const VIA_COMPONENT_ATRIB = VIA_PREFIX + "component"; 
const VIA_VER = "v1.0"
const VIA_DEVMODE = true; //LOGGING MORE INFORMATIONS
//const VIA_DEVMODE = false; //IMPORTANT!! CHANGE TO THIS IN PRODUCTION MODE

let AppInstance;
let Components = [];

function ViaAppLoad()
{
    console.log("RUNNING VIA.JS " + VIA_VER )
    if(VIA_DEVMODE == true) console.log("VIA.JS IS IN DEV MODE. ALL THINGS ARE LOGGED IN CONSOLE. PLEASE SHUTDOWN IN PRODUCTION MODE");

    let AppMainTable = document.querySelectorAll("*[" + VIA_APP_ATRIB + "]");
    if (AppMainTable.length != 1)
    {
        throw "You not have application or you have 2 and more application in html file"
    }

    AppInstance = AppMainTable[0];

    ViaAppObjectLoader(AppInstance);
    
    console.log("VIA.JS " + VIA_VER + " RUNNED")
}

function ViaAppObjectLoader()
{
    var components = ComponentAppSearcher()
    if(VIA_DEVMODE == true)console.log (components);
}

function ComponentCheck(obj) {
    if (obj == undefined) return false;
    if (obj == null) return false;
    if (obj == "") return false;
}

function ComponentAppSearcher()
{ 
    let components = [];
    let components_names = [];

    AppInstance.querySelectorAll("[" + VIA_COMPONENT_ATRIB + "]").forEach(ele => {
        components.push(ele);
        if(VIA_DEVMODE == true)console.log(ele.getAttribute(VIA_COMPONENT_ATRIB));
        components_names.push(ele.getAttribute(VIA_COMPONENT_ATRIB));
    })
    return components;
}

function ComponentSearcher(name)
{
    _founded_controllers = [];
    Components.forEach(ele => {
        if (ele.getAttribute(VIA_COMPONENT_ATRIB) == name) _founded_controllers.push(ele);
    });

    return _founded_controllers;
}

class ViaComponentsController
{
    _component_name = "";
    _components = null;
    _values = {}

    constructor(component_name, values = {})
    {
        if(VIA_DEVMODE == true) console.log("Try to create instance of ViaComponentControler");

        //Load Component
        this._component_name = component_name;
        
        if (this._component_name == null || this._component_name == undefined || this.component_name == "") throw "There you not set component name or set to null or empty string";

        this._components = ComponentSearcher(this._component_name);

        //Search Values
        this._values = values;
        this.SearchValsAndRedefine(this._values);

        if(VIA_DEVMODE == true) console.log("Succefully create instance of ViaComponentControler");
    }

    SearchValsAndRedefine(values)
    {

    }

    ForceReloadComponent()
    {
        
    }
}

ViaAppLoad();