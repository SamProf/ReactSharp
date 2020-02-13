(function () {


    function applyProps(element, a, id, $reactSharp) {
        if (a) {
            for (let aName in a) {
                if (!a.hasOwnProperty(aName)) {
                    continue;
                }

                let aValue = a[aName];


                if (aValue == null) {
                    if (element.hasAttribute(aName)) {
                        element.removeAttribute(aName);
                    }
                } else if (typeof aValue == "string") {
                    element.setAttribute(aName, aValue);
                    if (aName == 'value')
                    {
                        element.value = aValue;
                    }
                } else if (aValue.e == 1) {
                    // debugger;
                    var eventFunc = function () {
                        $reactSharp.fireEvent(id, aName);
                    };

                    element[aName] = eventFunc;
                }


            }
        }
    }


    var reactSharp = {

        init: function (ref, cmp, prerenderRef) {
            ref.$reactSharp = {
                items: [],
                prerenderRef: prerenderRef,
                firstRender: true,
                fireEvent(id, event) {
                    cmp.invokeMethodAsync('HandleEvent', id, event);
                }
            };
        },


        renderJson(ref, json) {
            $reactSharp = ref.$reactSharp;

            if ($reactSharp.firstRender && $reactSharp.prerenderRef && $reactSharp.prerenderRef instanceof HTMLElement)
            {
                $reactSharp.firstRender = false;
                $reactSharp.prerenderRef.style.display = 'none';
            }
            
            
            if (!json) {
                return;
            }
            var changes = json.c;
            if (!changes) {
                return;
            }

            for (var i = 0, len = changes.length; i < len; i++) {
                var item = changes[i];


                switch (item.c) {
                    case 1: {
                        var element = document.createElement(item.t);
                        $reactSharp.items[item.i] = element;
                        // var parentElement = item.p ? $reactSharp.items[item.p] : ref;
                        applyProps(element, item.a, item.i, $reactSharp);
                        
                        var parentElement = item.p ? $reactSharp.items[item.p] : ref;
                        if (item.b) {
                            debugger;
                            var beforeElement = $reactSharp.items[item.b];
                            parentElement.insertBefore(element, beforeElement);
                        } else {

                            parentElement.appendChild(element);
                        }
                        break;
                    }
                    case 2: {
                        var element = document.createTextNode(item.v);
                        $reactSharp.items[item.i] = element;
                        var parentElement = item.p ? $reactSharp.items[item.p] : ref;
                        if (item.b) {
                            var beforeElement = $reactSharp.items[item.b];
                            parentElement.insertBefore(element, beforeElement);
                        } else {
                            
                            parentElement.appendChild(element);
                        }

                        break;
                    }
                    case 3: {
                        var node = $reactSharp.items[item.i];
                        node.remove();
                        break;
                    }

                    case 4: {
                        var element = $reactSharp.items[item.i];
                        applyProps(element, item.a, item.i, $reactSharp);
                        break;
                    }

                    case 5: {
                        var element = $reactSharp.items[item.i];
                        element.nodeValue = item.v;
                        break;
                    }

                }

            }
        },

        renderJsonString(ref, jsonString) {
            reactSharp.renderJson(ref, JSON.parse(jsonString));
        }

    };
    window.reactSharp = reactSharp;
})();