var BattleValidate = BattleValidate || {

    stateArray: {
        1: "Bob", 2: "Bob", 3: "Jackson", 4: "Stuard", 5: "Sam", 6: "Sam"
    },

    stateString: "1:Bob;2:Bob;3:Jackson;4:Stuard;5:Sam;6:Sam;",

    customFormatter: function (value) {
        return BattleValidate.stateArray[value];
    }

};