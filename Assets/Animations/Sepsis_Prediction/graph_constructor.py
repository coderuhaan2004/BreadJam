import pandas as pd
from datetime import datetime
from collections import defaultdict

def prepare_patient_data(tables_train):
    """
    Transform CSV data into the required format for graph construction
    
    Expected output format for each patient:
    {
        'cond_hist': [[cond1, cond2], [cond3, cond4], ...],  # conditions per visit
        'procedures': [[proc1], [proc2, proc3], ...],         # procedures per visit
        'drugs': [[drug1, drug2], [drug3], ...],             # drugs per visit
        'adm_time': [timestamp1, timestamp2, ...]             # visit timestamps
    }
    """
    processed_data = []
    
    # Get unique patient IDs
    patients = tables_train['train']['person_demographics_episode']['person_id'].unique()
    
    print(f"Processing {len(patients)} patients...")
    
    for patient_id in patients:
        patient_data = defaultdict(list)
        
        # Collect events from different sources
        events = []
        
        for source in ['measurement_lab', 'measurement_meds', 'SepsisLabel', 'proceduresoccurances', 'drugsexposure']:
            if source in tables_train['train']:
                patient_records = tables_train['train'][source][
                    tables_train['train'][source]['person_id'] == patient_id
                ]
                if 'parameter_datetime' in patient_records.columns:
                    events.extend(patient_records['parameter_datetime'].dropna().tolist())
        
        events = sorted(list(set(events)))  # unique, sorted events
        
        if not events:  # Skip patients with no events
            continue
            
        for event_time in events:
            # Get conditions (from sepsis table)
            if 'train_sepsis' in tables_train:
                conditions = tables_train['train_sepsis'][
                    (tables_train['train_sepsis']['person_id'] == patient_id) &
                    (tables_train['train_sepsis']['parameter_datetime'] == event_time)
                ]['SepsisLabel'].tolist()  # or relevant condition code column
                patient_data['cond_hist'].append(conditions)
            
            # Get procedures
            if 'train_proceduresoccurances' in tables_train:
                procedures = tables_train['train_proceduresoccurances'][
                    (tables_train['train_proceduresoccurances']['person_id'] == patient_id) &
                    (tables_train['train_proceduresoccurances']['parameter_datetime'] == event_time)
                ]['procedure'].tolist()  # or relevant procedure code column
                patient_data['procedures'].append(procedures)
            
            # Get drugs
            if 'train_drugsexposure' in tables_train:
                drugs = tables_train['train_drugsexposure'][
                    (tables_train['train_drugsexposure']['person_id'] == patient_id) &
                    (tables_train['train_drugsexposure']['parameter_datetime'] == event_time)
                ]['drug_id'].tolist()  # or relevant drug code column
                patient_data['drugs'].append(drugs)
            
            # Get measurements
            if 'measurement_lab' in tables_train['train']:
                measurements = tables_train['train']['measurement_lab'][
                    (tables_train['train']['measurement_lab']['person_id'] == patient_id) &
                    (tables_train['train']['measurement_lab']['measurement_datetime'] == event_time)
                ].to_dict('records')
                patient_data['measurements'].append(measurements)
            
            # Add timestamp for this event
            patient_data['adm_time'].append(event_time)
        
        # Only add patients with complete data
        if all(len(patient_data[key]) == len(patient_data['adm_time']) for key in ['cond_hist', 'procedures', 'drugs', 'measurements']):
            processed_data.append(dict(patient_data))
    
    return processed_data

# # Create tokenizers for each type of medical code
# class SimpleTokenizer:
#     def __init__(self):
#         self.vocab = {}
#         self.next_id = 0
    
#     def fit(self, all_codes):
#         unique_codes = set()
#         for codes_list in all_codes:
#             for codes in codes_list:
#                 unique_codes.update(codes)
        
#         for code in sorted(unique_codes):
#             if code not in self.vocab:
#                 self.vocab[code] = self.next_id
#                 self.next_id += 1
    
#     def batch_encode_2d(self, codes_2d, padding=False):
#         return [[self.vocab.get(code, self.next_id-1) for code in codes] for codes in codes_2d]
    
#     def get_vocabulary_size(self):
#         return len(self.vocab)

# def create_tokenizers(processed_data):
#     tokenizers = {
#         'cond_hist': SimpleTokenizer(),
#         'procedures': SimpleTokenizer(),
#         'drugs': SimpleTokenizer()
#     }
    
#     # Fit tokenizers on all data
#     for key in tokenizers:
#         all_codes = [patient[key] for patient in processed_data]
#         tokenizers[key].fit(all_codes)
    
#     return tokenizers

# # Usage
# def prepare_data_for_graph():
#     # Load your data (using your existing data loading function)
#     tables_train = load_data_from_folder('path_to_your_data')
    
#     # Process the data
#     processed_data = prepare_patient_data(tables_train)
#     print(f"Processed {len(processed_data)} patients successfully")
    
#     # Create and fit tokenizers
#     tokenizers = create_tokenizers(processed_data)
    
#     return processed_data, tokenizers