(function () {
    let clickTimeout;
    let intervalDirection;
    let intervalTimeout;

    // Determines if it's a click or a press event
    let clickEvent = true;
    // Milliseconds before transitioning from click to press event
    const delay = 250;

    const numberInput = document.querySelector('.number-input [type="number"]');
    const decrementButton = document.querySelector('.number-input .btn-decrement');
    const incrementButton = document.querySelector('.number-input .btn-increment');


    const modifyNumber = () => {
        console.log('modifyNumber called with ', intervalDirection);
        if (intervalDirection === 'increment') {
            numberInput.stepUp()
        } else {
            numberInput.stepDown()
        }
    }

    const triggerDown = (direction) => {
        intervalDirection = direction;
        console.log('triggerDown called with ', intervalDirection);

        triggerInterval = () => {
            console.log('interval called with ', intervalDirection);
            clickEvent = false;
            intervalTimeout = setInterval(() => { modifyNumber(); }, [delay]);
        };

        clickTimeout = setTimeout(() => { triggerInterval(); }, delay);
    }

    const triggerUp = (direction) => {
        intervalDirection = direction;
        console.log('triggerUp called with ', intervalDirection);

        clearTimeout(clickTimeout);
        clearInterval(intervalTimeout);

        if (clickEvent === true) {
            console.log('triggerUp called modifyNumber with ', intervalDirection);
            modifyNumber();
        }
        clickEvent = true;
    }



    decrementButton.addEventListener("mousedown", () => { triggerDown("decrement"); }, false);
    incrementButton.addEventListener("mousedown", () => { triggerDown("increment"); }, false);
    decrementButton.addEventListener("mouseup", () => { triggerUp("decrement"); }, false);
    incrementButton.addEventListener("mouseup", () => { triggerUp("increment"); }, false);
})();