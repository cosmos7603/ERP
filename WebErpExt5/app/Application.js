/**
 * The main application class. An instance of this class is created by app.js when it calls
 * Ext.application(). This is the ideal place to handle application launch and initialization
 * details.
 */
Ext.define('ERP.Application', {
    extend: 'Ext.app.Application',
    
    name: 'ERP',

    stores: [
    //    'ERP.store.Menu'
        // TODO: add global / shared stores here
    ],
    
    launch: function () {
        // TODO - Launch the application

        String.prototype.ucfirst = function() {
            return this.charAt(0).toUpperCase() + this.substr(1);
        };
        String.prototype.pluralize = function () {
            return this + "s";
        };
    }
});
