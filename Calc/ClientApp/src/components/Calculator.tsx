// @ts-ignore
import React, {FC, useCallback, useEffect, useState} from "react";
import './Calculator.css'
import {Button, DropdownButton, Dropdown} from "react-bootstrap";
import {IAvailableOperations} from "../ServerModels/IAvailableOperations";

interface IProps {}
export const Calculator: FC<IProps> = (props: IProps) => {
    const calculateButtonName = '=';
    const clearButtonName = 'C';
    const mainButtonsNames = [
        '(', ')', clearButtonName,
        '7', '8', '9',
        '4', '5', '6',
        '1', '2', '3',
        '0', '.', calculateButtonName,
    ]
    const [availableOperations, setAvailableOperations] = useState<IAvailableOperations>(null);
    const [calculationProviders, setCalculationProviders] = useState<string[]>([]);
    const [currentProvider, setCurrentProvider] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState('');

    useEffect( () => {
        async function fetchData() {
            let response = await fetch('api/Calculator/GetAvailableOperations');
            let data = await response.json();
            setAvailableOperations(data);
            response = await fetch('api/Calculator/GetCalculationProviders');
            data = await response.json();
            setCalculationProviders(data);
            setCurrentProvider(data[0])
        }
        fetchData();
    }, [])

    const [expression, setExpression] = React.useState<string>('');
    const [sentExpression, setSentExpression] = React.useState<string>('');
    
    const closeBrackets = useCallback((expression) => {
        const openBracketCount = (expression.match('\\(') || []).length;
        const closeBracketCount = (expression.match('\\)') || []).length;
        return expression.padEnd(expression.length + openBracketCount - closeBracketCount, ')');
    }, []);
    
    const calculate = useCallback(async () => {
        const fixedExpression = expression.length === 0 ? '0' : closeBrackets(expression);
        const params = new URLSearchParams({
            expression: fixedExpression,
            calculationProviderName: currentProvider
        });
        const url = `api/Calculator/CalculateExpression?${params}`;
        const response = await fetch(url);
        const data = await response.text();
        if (response.ok){
            setSentExpression(`${fixedExpression}=${data}`)
            setExpression(data);
        }
        else {
            setErrorMessage(data)
            setExpression('');
        }
    }, [expression, currentProvider])
    
    const handleButtonClick = useCallback( (buttonName: string) => {
        if (buttonName === calculateButtonName)
            calculate();
        else
        {
            var text = buttonName === clearButtonName ? '' : expression + buttonName;
            setErrorMessage('');
            setExpression(text);
        }
    }, [expression, currentProvider]);
    
    const handleKeyDown = useCallback((event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter' )
            calculate();
    }, [expression, currentProvider]);
    
    const getButtonType = useCallback((buttonName: string) => {
        if (buttonName === calculateButtonName)
            return "success"
        else if (buttonName === clearButtonName)
            return "danger"
        return "secondary"
    }, [])
    
    return (
        <div className={"page"}>
            <div className="leftPart">
                <div className="expressionPanel">
                    <label className={"sentExpression"}>{sentExpression}</label>
                    <input className={"expressionInput"} placeholder='0' value={expression}
                           onChange={(e) => setExpression(e.target.value)}
                           onKeyDown={handleKeyDown}/>
                    <label className={"errorMessage"}>{errorMessage}</label>
                </div>
                <div className={"mainButtonsPanel"}>
                    {mainButtonsNames.map((buttonName) =>
                        <Button
                            className="button"
                            variant={getButtonType(buttonName)}
                            key={buttonName}
                            onClick={() => handleButtonClick(buttonName)}
                        >
                            {buttonName}
                        </Button>
                    )}

                </div>
            </div>
            
            <div className="rightPart">
                {availableOperations?.operations?.map((operation) =>
                    <Button
                        className="button"
                        variant="primary"
                        key={operation}
                        onClick={() => handleButtonClick(operation)}
                    >
                        {operation}
                    </Button>
                )}
                {availableOperations?.simpleOperations?.map((operation) =>
                    <Button
                        className="button"
                        variant="primary"
                        key={operation}
                        onClick={() => handleButtonClick(`${operation}(`)}
                    >
                        {operation}
                    </Button>
                )}
                <DropdownButton size={"lg"} variant={"secondary"} title={currentProvider} >
                    {calculationProviders.map(provider => 
                        <Dropdown.Item onClick={() => setCurrentProvider(provider)}>{provider}</Dropdown.Item>
                    )}
                </DropdownButton>
            </div>
        </div>
    );
}