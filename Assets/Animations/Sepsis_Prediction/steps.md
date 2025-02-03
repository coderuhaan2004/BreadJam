# Sepsis Prediction Project

## Steps
- 1: Explorative Data Analysis: 
    - Gather all measurements of particular patient_id

Status: Done EDA
- 2: Research on Architecture of the model:
    - diagnoses: SepsisLabel
    - medication: drugsexposure
    - procedures: procedureoccurrances
    - demographics: for person_id

- 3: Creating patient representations:
    - Numerical: [0.8 (glucose), 0.3 (blood pressure)]
    - Categorical: [1, 0, 1, 0, 0] (multi-hot vector for diagnosis and procedures)
    - Time: [7] (days since last visit)
    - Final Representation: [0.8, 0.3, 1, 0, 1, 0, 0, 7]

    Categorical Codes:
        - drugsexposure
        - diagnosis (Sepsis Label 0/1)
        - procedure-occurances
        - devices
    Numerical features:
        - measurement-meds
    Demographics:
        - person_id

    - For highly periodic or dense time-series data: Use Time2Vec as it better captures periodicity and works well with neural networks.

    - For irregular, sparse event data: Use time intervals, as they more explicitly represent relationships between events.

- 4: Concatenating vector embeddings from different parameters.
- 5: Creating a RNN/LSTM model.
